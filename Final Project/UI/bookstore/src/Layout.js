import { Outlet, Link } from "react-router-dom";

const Layout = () => {
  return (
    <>
      <nav>
        <ul>
          <li>
            <Link to="/login"> A little bit About the site</Link>
          </li>
          <li>
            <Link to="/login/searchBook">Search book to buy </Link>
          </li>
          <li>
            <Link to="/login/CreateUserBook">Creat book for sale</Link>
          </li>
          <li>
            <Link to="/login/DeleteUserBook">Delete book for sale</Link>
          </li>
          <li>
            <Link to="/login/MyBooks"> Show my books</Link>
          </li>
          <li>
            <Link to="/login/CreateMessage">Send message</Link>
          </li>
          <li>
            <Link to="/login/Messages">Show my messages</Link>
          </li>
          <li>
            <Link to="/login/SentMessages">Show messages I sent</Link>
          </li>
        </ul>
      </nav>

      <Outlet />
    </>
  )
};

export default Layout;