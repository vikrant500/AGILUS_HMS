import React, { useEffect } from "react";
import Footer from "../../components/footer";

const BookAppointment = () => {
  useEffect(() => {
    window.scrollTo(0, 0);
    window.Tally.loadEmbeds();
  }, []);
  return (
    <div className=" bg-[#fcfaf7] h-[940px] overflow-hidden">
      <iframe data-tally-src="https://tally.so/r/nW8vNj?transparentBackground=1" width="100%" height="100%" frameborder="0" marginheight="0" marginwidth="0" title="Book Free Consultation"></iframe>
    </div>
  );
};

export default BookAppointment;
