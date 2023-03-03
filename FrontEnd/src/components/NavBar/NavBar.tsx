import * as React from "react";
import "./nav-bar.css";

interface Props {
  text: string;
}

const NavBar: React.FC<Props> = ({ text }) => {
  return (
    <div className="nav-bar">
      <p className="nav-bar-text">{text}</p>
    </div>
  );
};

export default NavBar;
