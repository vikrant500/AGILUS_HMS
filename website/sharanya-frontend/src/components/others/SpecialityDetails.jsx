import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { examples } from "../../pages/treatments/constant/examples";
import WhyUs from "./WhyUs";
import FAQ from "./FAQ";
import Figures from "./Figures";
import WatchOurAds from "../../pages/home/components/WatchOurAds";
import { Link } from 'react-router-dom';

const SpecialityDetails = () => {
    const { treatmentId, specialityId } = useParams();
    const treatment = examples[treatmentId];
    const speciality = treatment?.specialities.find(
        (spec) => spec.title.toLowerCase().replace(/ /g, "-") === specialityId
    );

    const [diseaseDetails, setDiseaseDetails] = useState(null);

    useEffect(() => {
        if (speciality) {
            fetch(`http://localhost:4000/disease?name=${speciality.title}`)
                .then(response => response.json())
                .then(data => {
                    if (data.length > 0) {
                        const specificDisease = data.find(disease => disease.name === speciality.title);
                        if (specificDisease) {
                            setDiseaseDetails(specificDisease);
                        }
                    }
                })
                .catch(error => console.error("Error fetching disease details:", error));
        }
    }, [speciality]);

    if (!speciality || !diseaseDetails) {
        return <div>Speciality not found or Disease details not available.</div>;
    }

    return (
        <div className="bg-white text-black py-10 flex flex-col gap-4">
            <div className="flex justify-center">
                <div className="bg-grey shadow-lg rounded-lg p-8 max-w-4xl w-full">
                    <h3 className="text-3xl text-center font-bold text-orange-500 mb-4">{speciality.title}</h3>
                    <p className="text-center text-lg leading-relaxed">{diseaseDetails.description}</p>
                </div>
            </div>
            <div className="flex justify-center mt-6">
                <Link to="/book-appointment" className="w-full max-w-4xl">
                    <button
                        type="submit"
                        className="w-full py-3 flex justify-center bg-orange-500 text-white font-bold rounded-lg hover:bg-orange-700 focus:outline-none focus:ring-2 focus:ring-orange-400"
                    >
                        Book Appointment
                    </button>
                </Link>
            </div>
            <Figures />
            <WatchOurAds />
            <WhyUs />
        </div>
    );
};

export default SpecialityDetails;