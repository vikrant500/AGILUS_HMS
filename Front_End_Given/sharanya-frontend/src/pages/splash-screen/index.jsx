import React from "react";
import styles from './styles/index.module.css'
const SplashScreen = () => {
  
  return (
    <div className="bg-orange-500 h-[100vh] flex flex-col justify-center items-center">
      <div className='flex flex-col gap-4'>

        <h2 className="text-3xl text-white font-bold uppercase animate-bounce">
          Sharanya Care
        </h2>
      <div class={styles.loader}></div>
      </div>
    </div>
  );
};

export default SplashScreen;
