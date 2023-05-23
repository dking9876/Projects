import { Outlet, Link } from "react-router-dom";

const EntryLayout = () => {
  return (
    <>
      <nav>
        <ul>
          <li>
            <Link to="login">Login</Link>
          </li>
          <li>
            <Link to="/signup">SignIn</Link>
          </li>
        </ul>
      </nav>

      <Outlet />
    </>
  )
};

export default EntryLayout;