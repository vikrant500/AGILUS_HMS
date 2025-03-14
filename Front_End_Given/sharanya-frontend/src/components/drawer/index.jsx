"use client";
import * as React from "react";
import Box from "@mui/material/Box";
import Drawer from "@mui/material/Drawer";
import { IconButton } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import { Link, NavLink } from "react-router-dom";
import { sub_menu_buttons } from "../../constants/menu";
// import { assets } from "../../assets";

// Import the logo image
import Logo from "../../assets/images/sharanyacutwhitebg.jpg";

export default function ResponsiveDrawer() {
  const [state, setState] = React.useState({
    top: false,
    left: false,
    bottom: false,
    right: false,
  });

  const toggleDrawer = (anchor, open) => (event) => {
    if (
      event.type === "keydown" &&
      (event.key === "Tab" || event.key === "Shift")
    ) {
      return;
    }

    setState({ ...state, [anchor]: open });
  };

  const list = (anchor) => (
    <Box
      sx={{
        width: anchor === "top" || anchor === "bottom" ? "auto" : 250,
        display: "flex",
        flexDirection: "column",
        justifyContent: "space-between",
        gap: "4",
      }}
      role="presentation"
      onClick={toggleDrawer(anchor, false)}
      onKeyDown={toggleDrawer(anchor, false)}
    >
      <Link
        className="p-2 text-center hover:bg-orange-500 hover:text-white"
        to={"/"}
      >
        HOME
      </Link>

      <Link
        className="p-2 text-center hover:bg-orange-500 hover:text-white"
        to={"/doctors"}
      >
        DOCTORS
      </Link>
      <Link
        className="p-2 text-center hover:bg-orange-500 hover:text-white"
        to={"/blog"}
      >
        BLOG
      </Link>
      <Link
        className="p-2 text-center hover:bg-orange-500 hover:text-white"
        to={"/about"}
      >
        ABOUT
      </Link>
      {sub_menu_buttons.map((item, i) => (
        <Link
          key={i}
          className="p-2 text-center hover:bg-orange-500 hover:text-white"
          to={"/treatment/" + item.title.toLowerCase()}
        >
          {item.title}
        </Link>
      ))}

      <Link
        className="p-2 text-center bg-orange-500 text-white m-2 rounded-md"
        to={"/book-appointment"}
      >
        BOOK APPOINTMENT
      </Link>
    </Box>
  );

  return (
    <div className="sticky top-0 z-10">
      <div>
        <div>
          {["left"].map((anchor) => (
            <React.Fragment key={anchor}>
              <div className="p-4 bg-[#2C6975] text-white">
                <div className="flex justify-between items-center">
                  <Link to={"/"} className="flex items-center">
                    <div className="flex items-center duration-200 hover:scale-110 bg-white p-4 rounded-full">
                      <img
                        src={Logo}
                        alt="Sharanya Care Logo"
                        className="w-12 h-12 mr-2"
                      />
                      <h2 className="text-2xl md:text-4xl font-bold gradient_text text-orange-500">
                        Sharanya Care
                      </h2>
                    </div>
                  </Link>
                  <div className="gap-6 items-center hidden lg:flex">
                    <NavLink
                      to={"/"}
                      className={({ isActive }) =>
                        isActive
                          ? "text-orange-500 font-bold"
                          : "text-white hover:text-orange-500"
                      }
                    >
                      HOME
                    </NavLink>
                    <NavLink
                      to={"/doctors"}
                      className={({ isActive }) =>
                        isActive
                          ? "text-orange-500 font-bold"
                          : "text-white hover:text-orange-500"
                      }
                    >
                      DOCTORS
                    </NavLink>

                    <NavLink
                      to={"/about"}
                      className={({ isActive }) =>
                        isActive
                          ? "text-orange-500 font-bold"
                          : "text-white hover:text-orange-500"
                      }
                    >
                      ABOUT
                    </NavLink>
                    <NavLink
                      to={"/blog"}
                      className={({ isActive }) =>
                        isActive
                          ? "text-orange-500 font-bold"
                          : "text-white hover:text-orange-500"
                      }
                    >
                      BLOG
                    </NavLink>
                    <Link to={"/book-appointment"}>
                      <button className="bg-orange-500 border-2 border-orange-500 hover:border-white rounded-md px-4 py-2 hover:bg-orange-700 hover:text-white duration-200">
                        BOOK APPOINTMENT
                      </button>
                    </Link>
                  </div>
                  <div className="flex lg:hidden gap-4 items-center">
                    <IconButton
                      sx={
                        {
                          //  display: { xs: "block", lg: "none" }
                        }
                      }
                      onClick={toggleDrawer(anchor, true)}
                    >
                      <MenuIcon
                        fontSize="large"
                        sx={{
                          color: "white",
                        }}
                      />
                    </IconButton>
                  </div>
                </div>
                <Drawer
                  anchor={anchor}
                  open={state[anchor]}
                  onClose={toggleDrawer(anchor, false)}
                >
                  {list(anchor)}
                </Drawer>
              </div>
            </React.Fragment>
          ))}
        </div>
      </div>

      <div className="bg-white hidden md:flex p-4 justify-center gap-4 ">
        {sub_menu_buttons.map((item, i) => (
          <NavLink
            key={i}
            className={({ isActive }) =>
              isActive ? "text-orange-500 font-bold" : "text-black"
            }
            to={"/treatment/" + item.title.toLowerCase()}
          >
            {item.title}
          </NavLink>
        ))}
      </div>
    </div>
  );
}
