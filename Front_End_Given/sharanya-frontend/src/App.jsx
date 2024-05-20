import React, { useEffect, useState } from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./pages/home";
import Doctors from "./pages/doctors";
import ResponsiveDrawer from "./components/drawer";
import Footer from "./components/footer";
import About from "./pages/about";
import Treatments from "./pages/treatments";
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
              <Route path="/treatment/:id" element={<Treatments />} />
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
