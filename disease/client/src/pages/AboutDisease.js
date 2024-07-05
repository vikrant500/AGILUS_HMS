import Disease from "../Disease";
import { useEffect, useState } from "react";

export default function AboutDisease() {
    const [diseases, setDiseases] = useState([]);

    useEffect(() => {
        fetch('http://localhost:4000/disease').then(response => {
            response.json().then(diseases => {
                setDiseases(diseases);
            });
        });
    }, []);

    return (
        <>
            {diseases.length > 0 && diseases.map(disease => (
                <Disease {...disease} />
            ))}
        </>
    );
}
