import React from "react";
import { Link } from "react-router-dom";
import { sub_menu_buttons } from "../../constants/menu";
import { Facebook, Instagram, Twitter } from "@mui/icons-material";

const Footer = () => {
  const currentYear = new Date().getFullYear();
  return (
    <div className="flex flex-col gap-4 p-4 bg-primary text-white">
      <div className="">
        <h2 className="text-2xl md:text-4xl font-bold gradient_text">
          <Link to={"/"} className="">
            Sharanya Care
          </Link>
        </h2>
      </div>

      <div className="grid grid-cols-2 md:grid-cols-4 gap-8 p-0 md:p-8">
        <div className="flex flex-col gap-2">
          <h4 className="text-xl font-bold">Our Company</h4>
          <Link to={"/"} className="text-gray-200 uppercase">
            Home
          </Link>
          <Link to={"/doctors"} className="text-gray-200 uppercase">
            Doctors
          </Link>
          <Link to={"/about"} className="text-gray-200 uppercase">
            About
          </Link>
        </div>

        <div className="flex flex-col gap-2">
          <h4 className="text-xl font-bold">Treatments</h4>
          {sub_menu_buttons.map((button, index) => (
            <Link
              to={"/treatment/" + button.title.toLowerCase()}
              className="text-gray-200" 
            >
              {button.title}
            </Link>
          ))}
        </div>
        <div className="flex flex-col gap-2">
          <h4 className="text-xl font-bold">For Patients</h4>
          <Link to={"/book-appointment"} className="text-gray-200">
            BOOK FREE APPOINTMENT
          </Link>
        </div>

        <div className="flex flex-col gap-2">
          <h4 className="text-xl font-bold">Social Media</h4>
          <div className="flex gap-2">
            <Link to={"/"} className="text-gray-200">
              <div className="w-10 h-10 items-center justify-center bg-white rounded-full text-black flex">
                <Facebook fontSize="large" />
              </div>
            </Link>

            <Link to={"/"} className="text-gray-200">
              <div className="w-10 h-10 items-center justify-center bg-white rounded-full text-black flex">
                <Twitter fontSize="large" />
              </div>
            </Link>

            <Link to={"/"} className="text-gray-200">
              <div className="w-10 h-10 items-center justify-center bg-white rounded-full text-black flex">
                <Instagram fontSize="large" />
              </div>
            </Link>
          </div>
        </div>
      </div>
      <div className="flex gap-2 flex-col md:flex-row text-xs">
        <p className=" ">
          &copy; Copyright Sharanya {currentYear}. All Right Reserved.
        </p>
        <p>
          Developed by{" "}
          <Link
            to="https://devneural.com"
            target="_blank"
            className=" hover:underline"
          >
            Devneural Solutions Private Limited
          </Link>
        </p>
      </div>
    </div>
  );
};

export default Footer;
