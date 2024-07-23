import { useContext, useEffect, useState } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import { UserContext } from "../../UserContext";
import Header from "../../components/Header";
import config from "../../config/config";

export default function DiseasePage() {
  const [diseaseInfo, setDiseaseInfo] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const { userInfo } = useContext(UserContext);
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    window.scrollTo(0, 0);
    const fetchDisease = async () => {
      try {
        const response = await fetch(`${config.apiUrl}/disease/${id}`);
        if (!response.ok) {
          throw new Error(`Network response was not ok: ${response.statusText}`);
        }
        const data = await response.json();
        setDiseaseInfo(data);
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    };

    fetchDisease();
  }, [id]);

  const deleteDisease = async () => {
    const confirmed = window.confirm("Are you sure you want to delete this disease?");
    if (confirmed) {
      try {
        const response = await fetch(`${config.apiUrl}/disease/${id}`, {
          method: 'DELETE',
          credentials: 'include',
        });

        if (!response.ok) {
          const errorText = await response.text();
          throw new Error(`Failed to delete the disease: ${errorText}`);
        }

        navigate('/userlogin/about_disease'); // Redirect to the AboutDisease page
      } catch (error) {
        setError(`Failed to delete the disease: ${error.message}`);
      }
    }
  };

  if (loading) return <div className="text-center mt-4">Loading...</div>;
  if (error) return <div className="text-center mt-4 text-red-500">Error: {error}</div>;
  if (!diseaseInfo) return <div className="text-center mt-4">Disease not found.</div>;

  return (
    <div className="flex flex-col items-center p-4 bg-gray-100 min-h-screen">
      <header>
        <Header />
      </header>
      <h1 className="text-3xl font-bold mb-4">{diseaseInfo.name}</h1>
      <p className="text-lg mb-6">{diseaseInfo.description}</p>
      {userInfo && userInfo.id === diseaseInfo.doctor._id && (
        <div className="mt-4 flex space-x-4">
          <Link
            className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
            to={`/userLogin/editdisease/${diseaseInfo._id}`}
          >
            Edit this disease
          </Link>
          <button
            className="px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600"
            onClick={deleteDisease}
          >
            Delete this disease
          </button>
        </div>
      )}
    </div>
  );
}