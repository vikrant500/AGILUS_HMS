import React, { useEffect, useState } from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./pages/home";
import Doctors from "./pages/doctors";
import ResponsiveDrawer from "./components/drawer";
import Footer from "./components/footer";
import About from "./pages/about";
import Blog from "./pages/blog";
import Treatments from "./pages/treatments";
import SpecialityDetails from "./components/others/SpecialityDetails";
import BookAppointment from "./pages/book-appointment";
import SplashScreen from "./pages/splash-screen";

const App = () => {
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const timer = setTimeout(() => {
      setLoading(false);
    }, 2000);

    return () => {
      clearTimeout(timer);
    };
  }, []);

  return (
    <>
      {loading ? (
        <SplashScreen />
      ) : (
        <div className="max-w-[1920px] mx-auto">
          <BrowserRouter>
            <ResponsiveDrawer />
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/doctors" element={<Doctors />} />
              <Route path="/about" element={<About />} />
              <Route path="/blog" element={<Blog />} />
              <Route path="/treatment/:treatmentId" element={<Treatments />} />
              <Route path="/treatment/:treatmentId/specialities/:specialityId" element={<SpecialityDetails />} />

              <Route path="/book-appointment" element={<BookAppointment />} />
            </Routes>
            <Footer />
          </BrowserRouter>
        </div>
      )}
    </>
  );
};

export default App;
