import { useState, useContext } from "react";
import { Navigate } from "react-router-dom";
import { UserContext } from "../../UserContext";

export default function LoginPage() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [redirect, setRedirect] = useState(false);
  const { setUserInfo } = useContext(UserContext);

  async function login(ev) {
    ev.preventDefault();

    const response = await fetch('http://localhost:4000/login', 
    {
      method: 'POST',
      body: JSON.stringify({ username, password }),
      headers: { 'Content-Type': 'application/json' },
      credentials: 'include'
    });

    if (response.ok) {
      response.json().then(userInfo => {
        setUserInfo(userInfo);
        setRedirect(true);
      });
    } else {
      alert('Wrong credentials');
    }
  }

  if (redirect) {
    return <Navigate to={'/userlogin'} />;
  }

  // Inline styles
  const formStyle = {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center',
    width: '100%',
    maxWidth: '400px',
    margin: '15px auto',
    padding: '15px',
    borderRadius: '8px',
    boxShadow: '0 0 10px rgba(0,0,0,0.1)',
    backgroundColor: '#fff',
  };

  const inputStyle = {
    width: '100%',
    padding: '10px',
    marginBottom: '10px',
    borderRadius: '4px',
    border: '1px solid #ccc',
  };

  const buttonStyle = {
    padding: '10px 20px',
    border: 'none',
    borderRadius: '4px',
    backgroundColor: '#28a745',
    color: '#fff',  // Changed to white for better contrast
    fontSize: '16px',
    cursor: 'pointer',
  };

  const headingStyle = {
    fontSize: '32px', // Adjust the size as needed
    fontWeight: 'bold',
    marginBottom: '20px', // Add some margin to separate it from the inputs
  };

  return (
    <form style={formStyle} onSubmit={login}>
      <h1 style={headingStyle}>Login</h1>
      <input 
        type="text" 
        placeholder="Username" 
        value={username} 
        onChange={ev => setUsername(ev.target.value)}
        style={inputStyle} 
      />
      <input 
        type="password" 
        placeholder="Password" 
        value={password} 
        onChange={ev => setPassword(ev.target.value)}
        style={inputStyle} 
      />
      <button 
        type="submit" 
        style={buttonStyle}
      >
        Login
      </button>
    </form>
  );
}
