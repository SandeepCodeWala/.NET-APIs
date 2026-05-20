using System.Text;          // For Encoding

public class EncryptionMiddleware
{
    private readonly RequestDelegate _next;

    public EncryptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // 🔽 Decrypt Request
        if (context.Request.ContentLength > 0)
        {
            using var reader = new StreamReader(context.Request.Body);
            var encryptedBody = await reader.ReadToEndAsync();

            if (!string.IsNullOrEmpty(encryptedBody))
            {
                var decrypted = EncryptionHelper.Decrypt(encryptedBody);
                var bytes = Encoding.UTF8.GetBytes(decrypted);

                context.Request.Body = new MemoryStream(bytes);
            }
        }

        // 🔽 Capture Response
        var originalBody = context.Response.Body;
        using var newBody = new MemoryStream();
        context.Response.Body = newBody;

        await _next(context);

        // 🔼 Encrypt Response
        newBody.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(newBody).ReadToEndAsync();

        var encryptedResponse = EncryptionHelper.Encrypt(responseText);
        var bytesResponse = Encoding.UTF8.GetBytes(encryptedResponse);

        context.Response.Body = originalBody;
        await context.Response.Body.WriteAsync(bytesResponse);
    }
}