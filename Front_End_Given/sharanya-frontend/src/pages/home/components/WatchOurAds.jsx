import React from "react";

const WatchOurAds = () => {
  return (
    <div className="py-10 px-4 flex flex-col gap-8 bg-[#fcfaf7]">
      {/* <h3 className="text-2xl font-bold text-center">Watch Our Ads</h3> */}
      <div className="flex justify-center">
      <iframe
        width="560"
        height="315"
        src="https://www.youtube.com/embed/hXJ4gZshNys?si=VQ48aUe7J0Sei7rh"
        title="YouTube video player"
        frameborder="0"
        allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
        allowfullscreen
      ></iframe>
      </div>
    </div>
  );
};

export default WatchOurAds;
