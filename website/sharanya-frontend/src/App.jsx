import React, { useEffect, useState } from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./pages/home";
import Doctors from "./pages/doctors";
import ResponsiveDrawer from "./components/drawer";
import Footer from "./components/footer";
import About from "./pages/about";
import BlogPage from "./pages/blog"; // Updated import
import Treatments from "./pages/treatments";
import SpecialityDetails from "./components/others/SpecialityDetails";
import BookAppointment from "./pages/book-appointment";
import SplashScreen from "./pages/splash-screen";
import PostPage from "./pages/PostPage"; // Ensure this file exists
import IndexPage from "./pages/IndexPage"; // Ensure this file exists
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import CreatePost from "./pages/CreatePost";
import CreateDisease from "./pages/CreateDisease";
import EditPost from "./pages/EditPost";
import AboutDisease from "./pages/AboutDisease";
import DiseasePage from "./pages/DiseasePage";
import EditDisease from "./pages/EditDisease";
import { UserContextProvider } from './UserContext';



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
            <UserContextProvider>
              <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/doctors" element={<Doctors />} />
                <Route path="/about" element={<About />} />
                <Route path="/blog" element={<BlogPage />} /> {/* Updated route */}
                <Route path="/post/:id" element={<PostPage />} />
                <Route path="/treatment/:treatmentId" element={<Treatments />} />
                <Route path="/treatment/:treatmentId/specialities/:specialityId" element={<SpecialityDetails />} />
                <Route path="/book-appointment" element={<BookAppointment />} />
                <Route path="/userlogin" element={<IndexPage />} />
                <Route path="/userlogin/login" element={<LoginPage />} />
                <Route path="/userlogin/register" element={<RegisterPage />} />
                <Route path="/userlogin/create" element={<CreatePost />} />
                <Route path="/userlogin/post/:id" element={<PostPage />} />
                <Route path="/userlogin/createdisease" element={<CreateDisease />} />
                <Route path="/userlogin/edit/:id" element={<EditPost />} />
                <Route path="/userlogin/about_disease" element={<AboutDisease />} />
                <Route path="/userlogin/disease/:id" element={<DiseasePage />} />
                <Route path="/userlogin/editdisease/:id" element={<EditDisease />} />
              </Routes>
            </UserContextProvider>
            <Footer />
          </BrowserRouter>
        </div>
      )}
    </>
  );
};

export default App;