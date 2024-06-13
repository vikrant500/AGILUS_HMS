# AGILUS_HMS

# Getting Started

## Prerequisites

- Node.js installed
- MongoDB Atlas account

## Setup

1. **Installing the program:**

```BASH
   git clone https://github.com/vikrant500/AGILUS_HMS.git
```

2. Open the `Front_End_Given` folder and from there copy the `sharanya-frontend` into your local machine (Ex: copy the folder on desktop). This is to prevent committing cache.

3. Open the folder in preferred code editor (Ex: Visual Studio Code).

4. **Install dependencies:**
   
```bash
   npm install
```

5. **Check MongoDB connection:**

	Open `api/index.js` and verify the MongoDB connection string at line 22. If it doesn't work, verify the new IP address in MongoDB Atlas.
	
## Running the Application

1. **Open two terminals:**

   - **Terminal 1 (API):**
```bash
     cd api
     npm install
     nodemon index.js
```

   - **Terminal 2 (Client):**
```bash
     cd client
     npm install
     npm start
```
## Notes


- Ensure MongoDB Atlas IP Whitelist includes your current IP address.
- The client will be available at `http://localhost:3000`.
- The API will run on `http://localhost:4000`.
## Troubleshooting

- If you encounter issues with MongoDB connection, check the IP whitelist in your MongoDB Atlas account.
