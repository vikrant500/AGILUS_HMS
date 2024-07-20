import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { examples } from "../../pages/treatments/constant/examples";
import WhyUs from "./WhyUs";
import FAQ from "./FAQ";
import Figures from "./Figures";
import WatchOurAds from "../../pages/home/components/WatchOurAds";

const SpecialityDetails = () => {
    const { treatmentId, specialityId } = useParams();
    const treatment = examples[treatmentId];
    const speciality = treatment?.specialities.find(
        (spec) => spec.title.toLowerCase().replace(/ /g, "-") === specialityId
    );

    const [diseaseDetails, setDiseaseDetails] = useState(null);

    useEffect(() => {
        if (speciality) {
            fetch('http://localhost:4000/disease?name=${speciality.title}')
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
            <h3 className="text-2xl text-center font-bold">{speciality.title}</h3>
            <p className="text-center">{diseaseDetails.description}</p>
            <Figures />
            <WatchOurAds />
            <WhyUs />
        </div>
    );
};

export default SpecialityDetails;
