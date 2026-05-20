# MobileAppAPI — Complete API Documentation

**Base URL:** `https://net-apis.onrender.com`

All responses follow this standard format:
```json
{
  "status": 1,
  "message": "Success message",
  "data": { ... }
}
```
> `status: 1` = success | `status: 0` = error

---

## Table of Contents
1. [Plans](#1-plans)
2. [Cart](#2-cart)
3. [Orders](#3-orders)
4. [Requests](#4-requests)
5. [Mobile Services](#5-mobile-services)
6. [SIM Data](#6-sim-data)
7. [Contacts](#7-contacts)
8. [Delivery Address](#8-delivery-address)
9. [Mob Users](#9-mob-users)

---

## 1. Plans

### GET — Get All Plans
```
GET /api/plans
```
Returns all plans. Optionally filter by `category` and/or `simType` using request headers.

| Where  | Key        | Type   | Required | Values                              |
|--------|------------|--------|----------|-------------------------------------|
| Header | `category` | string | No       | `voicedata`, `dataonly`, `4gbackup` |
| Header | `simType`  | string | No       | `Physical`, `eSIM`                  |

**Example — Get all plans (no filter):**
```
GET https://net-apis.onrender.com/api/plans
```

**Example — Filter by category:**
```
GET https://net-apis.onrender.com/api/plans
Header: category = voicedata
```

**Example — Filter by both:**
```
GET https://net-apis.onrender.com/api/plans
Header: category = voicedata
Header: simType = eSIM
```

**Response:**
```json
{
  "status": 1,
  "message": "Plans fetched successfully",
  "data": [
    {
      "id": "1",
      "name": "Business Essential",
      "price": "30.00",
      "dataAllowance": "25",
      "duration": "month",
      "category": "voicedata",
      "planCode": "VEM0001",
      "simType": "Physical",
      "color": "bg-white",
      "priceColor": "text-gray-900",
      "features": [
        "5G network with 150Mbps maximum speed",
        "Speeds capped at 1.5Mbps after 25GB",
        "Unlimited national talk & text"
      ]
    }
  ]
}
```

---

## 2. Cart

### POST — Add to Cart
```
POST /api/plans/cart/add
```

| Where | Key        | Type    | Required | Description              |
|-------|------------|---------|----------|--------------------------|
| Body  | `planId`   | string  | Yes      | Plan ID (e.g. `"1"`)     |
| Body  | `quantity` | integer | Yes      | Must be greater than `0` |

**Request Body:**
```json
{
  "planId": "1",
  "quantity": 2
}
```

**Response:**
```json
{
  "status": 1,
  "message": "Item added to cart",
  "data": {
    "checkoutId": "d4f7a2b1-...",
    "totalAmount": 60.00
  }
}
```
> Save `checkoutId` — you will need it to fetch the cart.

---

### GET — Get Cart
```
GET /api/plans/cart
```

| Where  | Key           | Type   | Required | Description                            |
|--------|---------------|--------|----------|----------------------------------------|
| Header | `checkout_id` | string | Yes      | The `checkoutId` returned from Add Cart |

**Example:**
```
GET https://net-apis.onrender.com/api/plans/cart
Header: checkout_id = d4f7a2b1-...
```

**Response:**
```json
{
  "status": 1,
  "message": "Cart fetched successfully",
  "data": {
    "checkoutId": "d4f7a2b1-...",
    "items": [
      {
        "planId": "1",
        "planName": "Business Essential",
        "price": "30.00",
        "quantity": 2,
        "simType": "Physical"
      }
    ],
    "totalAmount": 60.00
  }
}
```

---

## 3. Orders

### GET — Order List
```
GET /api/orders?status={status}
```

| Where        | Key      | Type   | Required | Values                             |
|--------------|----------|--------|----------|------------------------------------|
| Query Param  | `status` | string | No       | `all`, `inprogress`, `completed`   |

**Example — Get all orders:**
```
GET https://net-apis.onrender.com/api/orders
```

**Example — Get only completed orders:**
```
GET https://net-apis.onrender.com/api/orders?status=completed
```

**Response:**
```json
{
  "status": 1,
  "message": "Orders fetched successfully",
  "data": [
    {
      "orderId": "ORD-000002072754",
      "orderDate": "2025-09-27",
      "amount": 330,
      "billingCycle": "per month",
      "status": "completed"
    }
  ]
}
```

---

### GET — Order Details
```
GET /api/orders/details
```

| Where  | Key       | Type   | Required | Description                               |
|--------|-----------|--------|----------|-------------------------------------------|
| Header | `orderId` | string | Yes      | e.g. `ORD-000002072754`                   |

**Example:**
```
GET https://net-apis.onrender.com/api/orders/details
Header: orderId = ORD-000002072754
```

**Response:**
```json
{
  "status": 1,
  "message": "Order details fetched successfully",
  "data": {
    "orderId": "ORD-000002072754",
    "orderDate": "2025-09-27",
    "amount": 330,
    "billingCycle": "per month",
    "status": "completed",
    "plans": [
      {
        "planName": "Business Everyday",
        "description": "Voice and Shared Data",
        "price": 40,
        "data": "50GB",
        "quantity": 2,
        "simType": "Physical SIM",
        "monthlyCost": 80,
        "deliveryStatus": "Delivered on September 27, 2025"
      }
    ],
    "tracking": [ ... ]
  }
}
```

---

### GET — Order Tracking
```
GET /api/orders/tracking
```

| Where  | Key       | Type   | Required | Description             |
|--------|-----------|--------|----------|-------------------------|
| Header | `orderId` | string | Yes      | e.g. `ORD-000002072754` |

**Example:**
```
GET https://net-apis.onrender.com/api/orders/tracking
Header: orderId = ORD-000002072754
```

**Response:**
```json
{
  "status": 1,
  "message": "Tracking fetched successfully",
  "data": [
    {
      "status": "Successful delivery",
      "location": "The Ponds, NSW",
      "date": "08-Oct-2025",
      "time": "1:20 PM",
      "completed": true
    }
  ]
}
```

---

## 4. Requests

### GET — Request List
```
GET /api/requests?status={status}
```

| Where       | Key      | Type   | Required | Values                                              |
|-------------|----------|--------|----------|-----------------------------------------------------|
| Query Param | `status` | string | No       | `all`, `in-progress`, `completed`, `completed-with-failure` |

**Example — Get all requests:**
```
GET https://net-apis.onrender.com/api/requests
```

**Example — Get only in-progress:**
```
GET https://net-apis.onrender.com/api/requests?status=in-progress
```

**Response:**
```json
{
  "status": 1,
  "message": "Requests fetched successfully",
  "data": [
    {
      "requestId": "REQ-000002072754",
      "title": "Suspend",
      "requestDate": "2025-10-14T12:33:00",
      "status": "in-progress"
    }
  ]
}
```

---

### GET — Request Details
```
GET /api/requests/details
```

| Where  | Key            | Type   | Required | Description                 |
|--------|----------------|--------|----------|-----------------------------|
| Header | `X-Request-Id` | string | Yes      | Must start with `REQ-`      |

**Example:**
```
GET https://net-apis.onrender.com/api/requests/details
Header: X-Request-Id = REQ-000002072753
```

**Response:**
```json
{
  "status": 1,
  "message": "Request details fetched successfully",
  "data": {
    "requestId": "REQ-000002072753",
    "title": "Cancel",
    "requestDate": "2025-09-27T12:33:00",
    "status": "completed-with-failure",
    "items": [
      {
        "mobileNumber": "0490 055 582",
        "status": "failed",
        "message": "Service activation failed"
      },
      {
        "mobileNumber": "0490 111 272",
        "status": "success",
        "message": ""
      }
    ]
  }
}
```

---

## 5. Mobile Services

### GET — Service List
```
GET /api/services/list
```
No parameters required.

**Example:**
```
GET https://net-apis.onrender.com/api/services/list
```

**Response:**
```json
{
  "status": 1,
  "message": "Mobile services fetched successfully",
  "data": [
    { "mobileNumber": "0490 055 582", "plan": "Business Power $65 180GB" },
    { "mobileNumber": "0490 111 272", "plan": "Business Best Value $55 120GB" },
    { "mobileNumber": "0490 345 115", "plan": "Business Everyday $40 50GB" }
  ]
}
```

---

### GET — Get Actions for a Service
```
GET /api/services/actions
```

| Where  | Key             | Type   | Required | Description                         |
|--------|-----------------|--------|----------|-------------------------------------|
| Header | `mobile_number` | string | Yes      | e.g. `0490 055 582` (with spaces)   |

**Example:**
```
GET https://net-apis.onrender.com/api/services/actions
Header: mobile_number = 0490 055 582
```

**Response:**
```json
{
  "status": 1,
  "message": "Actions fetched successfully",
  "data": [
    { "id": "2", "name": "Suspend", "icon": "ban", "color": "orange" },
    { "id": "4", "name": "Cancel", "icon": "circle-xmark", "color": "red" }
  ]
}
```

> Available numbers and their actions:
> - `0490 055 582` → Suspend, Cancel
> - `0490 111 272` → Activate, Resume, Swap SIM
> - `0490 345 115` → Suspend, Change Number, Change Network Profile

---

### POST — Submit Service Action
```
POST /api/services/submit
```

| Where | Key             | Type     | Required | Description                     |
|-------|-----------------|----------|----------|---------------------------------|
| Body  | `mobileNumber`  | string   | Yes      | e.g. `"0490 055 582"`           |
| Body  | `actionId`      | string   | Yes      | Action ID e.g. `"2"`            |
| Body  | `scheduledDate` | datetime | Yes      | ISO format e.g. `"2025-11-01T10:00:00"` |
| Body  | `reason`        | string   | Yes      | Reason for the action           |

**Request Body:**
```json
{
  "mobileNumber": "0490 055 582",
  "actionId": "2",
  "scheduledDate": "2025-11-01T10:00:00",
  "reason": "Employee left the company"
}
```

**Response:**
```json
{
  "status": 1,
  "message": "Service request submitted successfully",
  "data": {
    "mobileNumber": "0490 055 582",
    "actionId": "2",
    "scheduledDate": "2025-11-01T10:00:00",
    "reason": "Employee left the company"
  }
}
```

---

## 6. SIM Data

### GET — SIM List
```
GET /api/simdata/list
```

| Where  | Key        | Type   | Required | Values              |
|--------|------------|--------|----------|---------------------|
| Header | `sim_type` | string | Yes      | `new` or `port-in`  |

**Example:**
```
GET https://net-apis.onrender.com/api/simdata/list
Header: sim_type = new
```

**Response:**
```json
{
  "status": 1,
  "message": "SIM list fetched for new",
  "data": [
    { "id": "SIM1", "number": "8545123456789012345", "plan": "Business Power $65 180GB" },
    { "id": "SIM2", "number": "8545123456789012356", "plan": "Business Best Value $55 120GB" }
  ]
}
```

---

### POST — Activate New SIM
```
POST /api/simdata/activate-new
```

| Where | Key            | Type   | Required | Description                  |
|-------|----------------|--------|----------|------------------------------|
| Body  | `sIMNumberId`  | string | Yes      | SIM ID from the list         |
| Body  | `newSIMNumber` | string | Yes      | SIM number from the list     |
| Body  | `reason`       | string | Yes      | Reason for activation        |

**Request Body:**
```json
{
  "sIMNumberId": "SIM1",
  "newSIMNumber": "8545123456789012345",
  "reason": "New employee onboarding"
}
```

**Response:**
```json
{
  "status": 1,
  "message": "New SIM activation request sent successfully",
  "data": {
    "sIMNumberId": "SIM1",
    "newSIMNumber": "8545123456789012345",
    "reason": "New employee onboarding"
  }
}
```

---

### POST — Activate Port-In SIM
```
POST /api/simdata/activate-port
```

| Where | Key                            | Type   | Required | Description                   |
|-------|--------------------------------|--------|----------|-------------------------------|
| Body  | `sIMNumberId`                  | string | Yes      | SIM ID from the list          |
| Body  | `portInNumber`                 | string | Yes      | Number to port in             |
| Body  | `connectionType`               | string | Yes      | e.g. `Postpaid`, `Prepaid`    |
| Body  | `losingCarrierAccountNumber`   | string | Yes      | Account number with old carrier |

**Request Body:**
```json
{
  "sIMNumberId": "SIM3",
  "portInNumber": "0412345678",
  "connectionType": "Postpaid",
  "losingCarrierAccountNumber": "ACC-98765"
}
```

**Response:**
```json
{
  "status": 1,
  "message": "Port-in SIM activation request sent successfully",
  "data": { ... }
}
```

---

## 7. Contacts

### GET — Get All Contacts
```
GET /api/contacts
```
No parameters required.

---

### POST — Add Contact
```
POST /api/contacts
```

| Where | Key             | Type   | Required |
|-------|-----------------|--------|----------|
| Body  | `contactName`   | string | Yes      |
| Body  | `email`         | string | Yes      |
| Body  | `contactNumber` | string | Yes      |

**Request Body:**
```json
{
  "contactName": "John Smith",
  "email": "john@example.com",
  "contactNumber": "0490123456"
}
```

---

### PUT — Update Contact
```
PUT /api/contacts/{id}
```

| Where     | Key             | Type    | Required |
|-----------|-----------------|---------|----------|
| URL Param | `id`            | integer | Yes      |
| Body      | `contactName`   | string  | Yes      |
| Body      | `email`         | string  | Yes      |
| Body      | `contactNumber` | string  | Yes      |

**Example:**
```
PUT https://net-apis.onrender.com/api/contacts/1
```
```json
{
  "contactName": "John Updated",
  "email": "john.updated@example.com",
  "contactNumber": "0490999888"
}
```

---

## 8. Delivery Address

### GET — Get All Addresses
```
GET /api/deliveryAddress
```
No parameters required.

---

### POST — Add Address
```
POST /api/deliveryAddress
```

| Where | Key            | Type    | Required |
|-------|----------------|---------|----------|
| Body  | `addressLine1` | string  | Yes      |
| Body  | `suburb`       | string  | Yes      |
| Body  | `state`        | string  | Yes      |
| Body  | `postCode`     | integer | Yes      |

**Request Body:**
```json
{
  "addressLine1": "12 Main Street",
  "suburb": "Sydney",
  "state": "NSW",
  "postCode": 2000
}
```

---

### PUT — Update Address
```
PUT /api/deliveryAddress/{id}
```

| Where     | Key            | Type    | Required |
|-----------|----------------|---------|----------|
| URL Param | `id`           | integer | Yes      |
| Body      | `addressLine1` | string  | Yes      |
| Body      | `suburb`       | string  | Yes      |
| Body      | `state`        | string  | Yes      |
| Body      | `postCode`     | integer | Yes      |

---

## 9. Mob Users

### GET — Get All Users
```
GET /api/mobUsers
```

### GET — Get User by ID
```
GET /api/mobUsers/{id}
```

| Where     | Key  | Type  | Required |
|-----------|------|-------|----------|
| URL Param | `id` | long  | Yes      |

**Example:**
```
GET https://net-apis.onrender.com/api/mobUsers/1
```

---

### POST — Add User
```
POST /api/mobUsers
```

| Where | Key          | Type    | Required |
|-------|--------------|---------|----------|
| Body  | `uid`        | string  | Yes      |
| Body  | `firstName`  | string  | Yes      |
| Body  | `lastName`   | string  | Yes      |
| Body  | `email`      | string  | No       |
| Body  | `primMobNo`  | string  | Yes      |
| Body  | `seconMobNo` | string  | No       |
| Body  | `isActive`   | boolean | Yes      |
| Body  | `isLocked`   | boolean | Yes      |

**Request Body:**
```json
{
  "uid": "U004",
  "firstName": "Alice",
  "lastName": "Brown",
  "email": "alice@example.com",
  "primMobNo": "0490777888",
  "isActive": true,
  "isLocked": false
}
```

---

### PUT — Update User
```
PUT /api/mobUsers/{id}
```
Same body fields as POST. URL param `id` required.

---

### DELETE — Delete User
```
DELETE /api/mobUsers/{id}
```

| Where     | Key  | Type | Required |
|-----------|------|------|----------|
| URL Param | `id` | long | Yes      |

**Example:**
```
DELETE https://net-apis.onrender.com/api/mobUsers/3
```

**Response:**
```json
{
  "status": 1,
  "message": "User deleted successfully"
}
```

---

## Quick Reference — All Endpoints

| Method | Endpoint                        | Params / Headers                          |
|--------|---------------------------------|-------------------------------------------|
| GET    | `/api/plans`                    | Header: `category`, `simType` (optional)  |
| POST   | `/api/plans/cart/add`           | Body: `planId`, `quantity`                |
| GET    | `/api/plans/cart`               | Header: `checkout_id`                     |
| GET    | `/api/orders`                   | Query: `status`                           |
| GET    | `/api/orders/details`           | Header: `orderId`                         |
| GET    | `/api/orders/tracking`          | Header: `orderId`                         |
| GET    | `/api/requests`                 | Query: `status`                           |
| GET    | `/api/requests/details`         | Header: `X-Request-Id`                    |
| GET    | `/api/services/list`            | None                                      |
| GET    | `/api/services/actions`         | Header: `mobile_number`                   |
| POST   | `/api/services/submit`          | Body: `mobileNumber`, `actionId`, `scheduledDate`, `reason` |
| GET    | `/api/simdata/list`             | Header: `sim_type` (`new` / `port-in`)    |
| POST   | `/api/simdata/activate-new`     | Body: `sIMNumberId`, `newSIMNumber`, `reason` |
| POST   | `/api/simdata/activate-port`    | Body: `sIMNumberId`, `portInNumber`, `connectionType`, `losingCarrierAccountNumber` |
| GET    | `/api/contacts`                 | None                                      |
| POST   | `/api/contacts`                 | Body: `contactName`, `email`, `contactNumber` |
| PUT    | `/api/contacts/{id}`            | Body: `contactName`, `email`, `contactNumber` |
| GET    | `/api/deliveryAddress`          | None                                      |
| POST   | `/api/deliveryAddress`          | Body: `addressLine1`, `suburb`, `state`, `postCode` |
| PUT    | `/api/deliveryAddress/{id}`     | Body: `addressLine1`, `suburb`, `state`, `postCode` |
| GET    | `/api/mobUsers`                 | None                                      |
| GET    | `/api/mobUsers/{id}`            | URL param: `id`                           |
| POST   | `/api/mobUsers`                 | Body: user fields                         |
| PUT    | `/api/mobUsers/{id}`            | Body: user fields                         |
| DELETE | `/api/mobUsers/{id}`            | URL param: `id`                           |
