import { useEffect, useState } from "react";
import { Navigate, useParams } from "react-router-dom";

export default function EditDisease() {
  const { id } = useParams();
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [redirect, setRedirect] = useState(false);

  useEffect(() => {
    fetch(`http://localhost:4000/disease/${id}`)
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
      const response = await fetch('http://localhost:4000/disease', {
        method: 'PUT',
        body: data,
        credentials: 'include',
      });
      if (response.ok) {
        setRedirect(true);
      } else {
        console.error('Failed to update the disease', response.statusText);
      }
    } catch (error) {
      console.error('Error during fetch', error);
    }
  }

  if (redirect) {
    return <Navigate to={`/disease/${id}`} />;
  }

  return (
    <form onSubmit={updateDisease}>
      <input
        type="text"
        placeholder="Name"
        value={name}
        onChange={ev => setName(ev.target.value)}
      />
      <input
        type="text"
        placeholder="Description"
        value={description}
        onChange={ev => setDescription(ev.target.value)}
      />
      <button style={{ marginTop: '5px' }}>Update Disease</button>
    </form>
  );
}