import { Outlet, Link } from "react-router-dom";
import './App.css';
const EntryLayout = () => {
  return (
    <>
      <nav>
        <ul className="custom-list">
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