import { Link, useLocation } from "react-router-dom";
import { useContext, useEffect } from "react";
import { UserContext } from "./UserContext";

export default function Header() {
  const { setUserInfo, userInfo } = useContext(UserContext);
  const location = useLocation();

  useEffect(() => {
    fetch('http://localhost:4000/profile', {
      credentials: 'include',
    }).then(response => {
      response.json().then(userInfo => {
        setUserInfo(userInfo);
      });
    });
  }, [setUserInfo]);

  function logout() {
    fetch('http://localhost:4000/logout', {
      credentials: 'include',
      method: 'POST',
    }).then(() => {
      setUserInfo(null);
      // Redirect to home page after logout
      window.location.href = '/'; // Directly setting href for full page reload
    });
  }

  const username = userInfo?.username;

  const handleLogoClick = () => {
    // Reset search query and fetch all posts
    const queryParams = new URLSearchParams(location.search);
    queryParams.set('q', ''); // Set search query to empty string
    queryParams.set('page', '1'); // Reset page to 1
    window.location.href = `/?${queryParams.toString()}`; // Full page reload
  };

  return (
    <header>
      <Link to="/" className="logo" onClick={handleLogoClick}>
        MyBlog
      </Link>
      <nav>
        {username ? (
          <>
            <Link to="/create">Create new post</Link>
            <a onClick={logout}>Logout</a>
          </>
        ) : (
          <>
            <Link to="/login">Login</Link>
            <Link to="/register">Register</Link>
          </>
        )}
      </nav>
    </header>
  );
}
