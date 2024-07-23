import ReactQuill from "react-quill";
import "react-quill/dist/quill.snow.css";
import { useState } from "react";
import { Navigate } from "react-router-dom";
import config from '../../config/config'; // Import the config file

const modules = {
  toolbar: [
    [{ header: [1, 2, false] }],
    ["bold", "italic", "underline", "strike", "blockquote"],
    [{ list: "ordered" }, { list: "bullet" }, { indent: "-1" }, { indent: "+1" }],
    ["link", "image"],
    ["clean"],
  ],
};

const formats = [
  "header",
  "bold",
  "italic",
  "underline",
  "strike",
  "blockquote",
  "list",
  "bullet",
  "indent",
  "link",
  "image",
];

export default function CreateDisease() {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [redirect, setRedirect] = useState(false);

  async function createNewDisease(ev) {
    const data = new FormData();
    data.set("name", name);
    data.set("description", description);

    ev.preventDefault();

    const response = await fetch(`${config.apiUrl}/disease`, {
      method: "POST",
      body: data,
      credentials: "include",
    });

    if (response.ok) {
      setRedirect(true);
      alert("Disease created successfully");
    } else {
      alert("Failed to create disease");
    }
  }

  if (redirect) {
    return <Navigate to={"/userlogin/about_disease"} />;
  }

  return (
    <form
      onSubmit={createNewDisease}
      className="max-w-3xl mx-auto bg-white p-6 rounded shadow-md"
    >
      <h2 className="text-2xl font-bold mb-4">Create New Disease</h2>
      <div className="mb-4">
        <label className="block text-gray-700 mb-2">Name</label>
        <input
          type="text"
          placeholder="Name"
          value={name}
          onChange={(ev) => setName(ev.target.value)}
          className="w-full p-2 border border-gray-300 rounded"
        />
      </div>
      <div className="mb-4">
        <label className="block text-gray-700 mb-2">Description</label>
        <textarea
          placeholder="Description"
          value={description}
          onChange={(ev) => setDescription(ev.target.value)}
          className="w-full p-2 border border-gray-300 rounded h-60"
        />
        {/* Uncomment the below lines if you want to use ReactQuill for description input */}
        {/* <ReactQuill
          value={description}
          onChange={(newValue) => setDescription(newValue)}
          modules={modules}
          formats={formats}
          className="h-60 w-full"
        /> */}
      </div>
      <button
        type="submit"
        className="w-full bg-blue-500 text-white py-2 rounded hover:bg-blue-600"
      >
        Create Disease
      </button>
    </form>
  );
}