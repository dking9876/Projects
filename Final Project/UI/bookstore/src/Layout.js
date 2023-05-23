import { Outlet, Link } from "react-router-dom";

const Layout = () => {
  return (
    <>
      <nav>
        <ul>
          <li>
            <Link to="/">Main</Link>
          </li>
          <li>
            <Link to="/signup">Signup</Link>
          </li>
          <li>
            <Link to="/login">Login</Link>
          </li>
          <li>
            <Link to="/searchBook">SearchBook</Link>
          </li>
          <li>
            <Link to="/CreateUserBook">CreateUserBook</Link>
          </li>
          <li>
            <Link to="/DeleteUserBook">DeleteUserBook</Link>
          </li>
          <li>
            <Link to="/MyBooks">MyBooks</Link>
          </li>
          <li>
            <Link to="/CreateMessage">CreateMessage</Link>
          </li>
          <li>
            <Link to="/Messages">Messages</Link>
          </li>
          <li>
            <Link to="/SentMessages">SentMessages</Link>
          </li>
        </ul>
      </nav>

      <Outlet />
    </>
  )
};

export default Layout;