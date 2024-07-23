import { useEffect, useState } from "react";
import { Navigate, useParams } from "react-router-dom";
import config from "../../config/config";

export default function EditDisease() {
  const { id } = useParams();
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [redirect, setRedirect] = useState(false);
  const [notification, setNotification] = useState(''); // State for notification message

  useEffect(() => {
    fetch(`${config.apiUrl}/disease/${id}`)
      .then(response => response.json())
      .then(diseaseInfo => {
        setName(diseaseInfo.name);
        setDescription(diseaseInfo.description);
      });
  }, [id]);

  async function updateDisease(ev) {
    ev.preventDefault();
    const data = new FormData();
    data.set('name', name);
    data.set('description', description);
    data.set('id', id);

    try {
      const response = await fetch(`${config.apiUrl}/disease`, {
        method: 'PUT',
        body: data,
        credentials: 'include',
      });
      if (response.ok) {
        setNotification('Disease updated successfully!'); // Set notification message
        setTimeout(() => {
          setNotification(''); // Clear notification after 3 seconds
          setRedirect(true);
        }, 3000);
      } else {
        console.error('Failed to update the disease', response.statusText);
      }
    } catch (error) {
      console.error('Error during fetch', error);
    }
  }

  if (redirect) {
    return <Navigate to={`/userlogin/about_disease`} />;
  }

  return (
    <div className="flex justify-center items-center min-h-screen bg-gray-100 p-4">
      <form onSubmit={updateDisease} className="bg-white p-8 rounded-lg shadow-lg w-full max-w-4xl">
        <h2 className="text-2xl font-bold mb-6">Edit Disease</h2>
        {notification && (
          <div className="mb-4 text-green-600 font-medium">
            {notification}
          </div>
        )}
        <div className="mb-6">
          <label htmlFor="name" className="block text-sm font-medium text-gray-700">Name</label>
          <input
            type="text"
            id="name"
            placeholder="Enter disease name"
            value={name}
            onChange={ev => setName(ev.target.value)}
            className="mt-2 block w-full h-12 px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 resize-none"
          />
        </div>
        <div className="mb-6">
          <label htmlFor="description" className="block text-sm font-medium text-gray-700">Description</label>
          <textarea
            id="description"
            placeholder="Enter disease description"
            value={description}
            onChange={ev => setDescription(ev.target.value)}
            className="mt-2 block w-full px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 resize-y min-h-[200px]"
          />
        </div>
        <button
          type="submit"
          className="w-full py-3 px-6 bg-blue-500 text-white rounded-md hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500"
        >
          Update Disease
        </button>
      </form>
    </div>
  );
}