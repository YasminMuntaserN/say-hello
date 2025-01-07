# Real-Time Chat Application

## Overview
The **Real-Time Chat Application** is a cutting-edge, desktop-style web application designed to provide seamless and secure communication. Powered by a robust **.NET 8** backend and a **React Vite** frontend, the application leverages modern technologies to deliver a fast, reliable, and feature-rich user experience.  
This application supports real-time messaging for private and group conversations, enhanced by advanced features such as email-based authentication, user management, and instant notifications.

---

## Key Features

### **Authentication and Account Management**
- **Email-Based Registration:**
  - Users register with their email, verify their accounts through a secure email link, and gain full access to the application.
- **Password Recovery:**
  - Includes a "Forgot Password" feature that sends a secure email link for resetting passwords.
- **User Profile Management:**
  - Edit profile details and update passwords.
  - Block or archive other users for better organization.

### **Real-Time Messaging**
- **Private Chats:**
  - Instantaneous one-on-one communication with real-time updates.
- **Group Chats:**
  - Dynamic group messaging where all participants receive messages instantly.

### **Search and Interaction**
- **User Search:**
  - Find other users by name or email to initiate chats.
- **Interactive Features:**
  - Create groups for collaboration.
  - Block or archive chats for streamlined user experience.

---

## Technical Architecture

### **Backend**
- **Framework:** ASP.NET Core 8
- **Database:** SQL Server, managed with Entity Framework Core
- **Real-Time Communication:** 
  - Implemented using SignalR for two-way communication between server and clients.
- **Validation and Mapping:**  
  - **FluentValidation:** Ensures robust data validation for DTOs.  
  - **Mapster:** Simplifies mapping between DTOs and entities.
- **Email Services:**  
  - Built-in email functionality for account verification and password recovery.

### **Frontend**
- **Framework:** React with Vite for fast builds and optimized performance.
- **Styling:** Tailwind CSS for sleek and modern UI.
- **Libraries and Tools:**  
  - **React Query:** Efficient state management and seamless data fetching.  
  - **React Router:** Handles navigation and routing between pages.  
  - **React Hook Form:** Simplifies form validation and data handling.  
  - **React Hot Toast:** Displays elegant toast notifications.  
  - **SignalR Client:** Connects the React application to the backend for real-time updates.

---

## How SignalR Powers Real-Time Features
 **The SignalR library plays a critical role in enabling real-time communication for the chat application:**
**Persistent Connection:** SignalR establishes a continuous connection between the server and clients, ensuring real-time delivery of messages.
**Message Flow:**
When a user sends a message, it is transmitted to the SignalR Hub on the server.
The hub processes the message and instantly broadcasts it to the intended recipient(s).
All participants, whether in private or group chats, receive the message in real time.
**Group Chat Synchronization:**
SignalR manages group communication, ensuring every group member receives messages simultaneously, maintaining consistent state across all devices.

**By utilizing WebSockets, SignalR guarantees efficient and seamless communication, even under varying network conditions.**

## Project Workflow

### **Registration and Verification**
- A user creates an account, triggering a verification email with a secure link.
     <div align="center">
    <img src="https://imgur.com/9Jf00pS.jpg" alt="Registration Verification" />
  </div>
    <div align="center">
    <img src="https://imgur.com/1xCNxeb.jpg" alt="Registration Verification" />
  </div>
  <div align="center">
    <img src="https://imgur.com/0SSEdVW.jpg" alt="Registration Verification" />
  </div>

- Upon verification, the user gains access to their dashboard.  
  <div align="center">
    <img src="https://imgur.com/A1PQswv.jpg" alt="User Dashboard" />
  </div>

---

### **Real-Time Chat**
- Users can search for other registered users and initiate chats.  
  <div align="center">
    <img src="https://imgur.com/6VNtS7f.jpg" alt="Search Users" />
    <img src="https://imgur.com/hnE6xuA.jpg" alt="Chat Example" />
  </div>

- Group creation enables collaborative conversations in real time.  
  <div align="center">
    <img src="https://imgur.com/8rTmw3y.jpg" alt="Group Creation" />
    <img src="https://imgur.com/rDZ7OAt.jpg" alt="Group Chat Example" />
  </div>

---

### **Message Delivery**
- All messages are stored in the SQL Server database for retrieval and archival.
- SignalR ensures instantaneous delivery and updates for all connected users.

---

### **Account Management**
- Users can block or archive conversations.
- Profiles can be edited to update information or reset passwords.  
  <div align="center">
    <img src="https://imgur.com/dt7Qxt6.jpg" alt="Edit Profile" />
    <img src="https://imgur.com/NEzABbg.jpg" alt="Reset Password" />
  </div>

## 🚀 Live Demo
<a href="" alt="demo">🔗click me 😊!</a>
