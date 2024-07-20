import { useContext, useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";
import { UserContext } from "../UserContext";

export default function DiseasePage() {
  const [diseaseInfo, setDiseaseInfo] = useState(null);
  const { userInfo } = useContext(UserContext);
  const { id } = useParams();

  useEffect(() => {
    fetch(`http://localhost:4000/disease/${id}`)
      .then(response => response.json())
      .then(data => setDiseaseInfo(data));
  }, [id]);

  if (!diseaseInfo) return '';

  return (
    <div className="disease-page">
      <h1>{diseaseInfo.name}</h1>
      <p>{diseaseInfo.description}</p>
      {userInfo.id === diseaseInfo.doctor._id && (
        <div className="edit-row">
          <Link className="edit-btn" to={`/editdisease/${diseaseInfo._id}`}>
            Edit this disease
          </Link>
        </div>
      )}
    </div>
  );
}