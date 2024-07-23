import { Link } from "react-router-dom";
import { useContext, useEffect } from "react";
import { UserContext } from "../../UserContext";

export default function Header() {
  const { setUserInfo, userInfo } = useContext(UserContext);

  useEffect(() => {
    fetch('http://localhost:4000/profile', {
      credentials: 'include',
    }).then(response => {
      response.json().then(userInfo => {
        setUserInfo(userInfo);
      });
    });
  }, []);

  function logout() {
    fetch('http://localhost:4000/logout', {
      credentials: 'include',
      method: 'POST',
    });
    setUserInfo(null);
  }

  const username = userInfo?.username;

  // Inline styles
  const headerStyle = {
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: '10px 20px',
    width: '100%',
    boxSizing: 'border-box',
  };

  const logoButtonStyle = {
    display: 'inline-block',
    fontWeight: 'bold',
    fontSize: '1.5rem',
    textDecoration: 'none',
    color: 'inherit',
    padding: '10px 20px',
    transition: 'color 0.3s, background-color 0.3s',
    backgroundColor: 'transparent',
    border: '2px solid #6bbf7c',
    borderRadius: '9999px', // Makes the button oval
    cursor: 'pointer',
  };

  const navContainerStyle = {
    display: 'flex',
    gap: '20px',
    marginLeft: 'auto', // Pushes the navigation to the right
  };

  const linkStyle = {
    textDecoration: 'none',
    color: 'inherit',
    padding: '5px 10px',
    transition: 'color 0.3s',
  };

  const linkHoverStyle = {
    color: '#6bbf7c', // Change to desired hover color
  };

  return (
    <header style={headerStyle}>
      <Link to="/userlogin">
        <button
          style={logoButtonStyle}
          onMouseOver={e => {
            e.target.style.color = '#ffffff';
            e.target.style.backgroundColor = '#6bbf7c';
          }}
          onMouseOut={e => {
            e.target.style.color = 'inherit';
            e.target.style.backgroundColor = 'transparent';
          }}
        >
          MyBlog
        </button>
      </Link>
      <div style={navContainerStyle}>
        {username ? (
          <>
            <Link
              to="/userlogin/create"
              style={linkStyle}
              onMouseOver={e => e.target.style.color = linkHoverStyle.color}
              onMouseOut={e => e.target.style.color = ''}
            >
              Create new post
            </Link>
            <Link
              to="/userlogin/createdisease"
              style={linkStyle}
              onMouseOver={e => e.target.style.color = linkHoverStyle.color}
              onMouseOut={e => e.target.style.color = ''}
            >
              Create new disease
            </Link>
            <Link
              to="/userlogin/about_disease"
              style={linkStyle}
              onMouseOver={e => e.target.style.color = linkHoverStyle.color}
              onMouseOut={e => e.target.style.color = ''}
            >
              About diseases
            </Link>
            <a
              onClick={logout}
              style={linkStyle}
              onMouseOver={e => e.target.style.color = linkHoverStyle.color}
              onMouseOut={e => e.target.style.color = ''}
            >
              Logout
            </a>
          </>
        ) : (
          <>
            <Link
              to="/userlogin/login"
              style={linkStyle}
              onMouseOver={e => e.target.style.color = linkHoverStyle.color}
              onMouseOut={e => e.target.style.color = ''}
            >
              Login
            </Link>
            <Link
              to="/userlogin/register"
              style={linkStyle}
              onMouseOver={e => e.target.style.color = linkHoverStyle.color}
              onMouseOut={e => e.target.style.color = ''}
            >
              Register
            </Link>
          </>
        )}
      </div>
    </header>
  );
}