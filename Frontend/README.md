# Frontend

React frontend application for managing candidates and their skills.

---

# Technologies Used

- React
- TypeScript
- Vite
- Tailwind CSS
- Material Tailwind
- Axios

---

# Features

- Display all candidates
- Search candidates by name
- Filter candidates by skills
- Add new candidate
- Edit candidate skills
- Delete candidate
- Add new skills
- Frontend form validation for:
  - email format
  - contact number format
  - date format
  - required fields

---

# Setup Instructions

## 1. Install dependencies

```bash
npm install
```

---

## 2. Create `.env` file

Create a `.env` file in the frontend root directory:

```env
VITE_API_URL='https://localhost:{port_from_backend}/api'
```

---

## 3. Run the application

```bash
npm run dev
```

Frontend will run on:

```txt
http://localhost:5173
```

---

# API Communication

Frontend communicates with the ASP.NET Core backend through REST API endpoints using Axios.
