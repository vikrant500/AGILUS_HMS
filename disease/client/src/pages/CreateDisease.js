import ReactQuill from "react-quill";
import "react-quill/dist/quill.snow.css";
import { useState } from "react";
import { Navigate } from "react-router-dom";

const modules = {
    toolbar: [
      [{ header: [1, 2, false] }],
      ['bold', 'italic', 'underline', 'strike', 'blockquote'],
      [
        { list: 'ordered' },
        { list: 'bullet' },
        { indent: '-1' },
        { indent: '+1' },
      ],
      ['link', 'image'],
      ['clean'],
    ],
};

const formats = [
    'header',
    'bold', 'italic', 'underline', 'strike', 'blockquote',
    'list', 'bullet', 'indent',
    'link', 'image'
  ];

export default function CreateDisease() {

    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [redirect, setRedirect] = useState(false);

    async function createNewDisease(ev){

      const data = new FormData();

      data.set('name', name);
      data.set('description', description);

      ev.preventDefault();
      
      const response = await fetch('http://localhost:4000/disease', {
        method: 'POST',
        body: data,
        credentials: 'include'
      });

      if (response.ok) {
        setRedirect(true);
        alert('Disease created successfully');
      } else {
        alert('Failed to create disease');
      }

    };

    if (redirect) {
        return <Navigate to={'/about_disease'} />
    }

    return (
        <form onSubmit={createNewDisease}>
            <input type="text" placeholder="name" value={name} onChange={ev => setName(ev.target.value)}/>
            <input type='text' placeholder='description' value={description} onChange={ev => setDescription(ev.target.value)}/>
            {/* <ReactQuill value={description} onChange={newValue => setDescription(newValue)} modules={modules} formats={formats}/> */}
            <button type="submit" style={{marginTop: '10px'}}>Create Disease</button>
        </form>
    )
}