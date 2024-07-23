import ReactQuill from "react-quill";
import 'react-quill/dist/quill.snow.css';
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

export default function CreatePost() {
  const [title, setTitle] = useState('');
  const [summary, setSummary] = useState('');
  const [content, setContent] = useState('');
  const [files, setFiles] = useState('');
  const [tags, setTags] = useState('');
  const [redirect, setRedirect] = useState(false);
  const [notification, setNotification] = useState(''); // State for notification message

  async function createNewPost(ev) {
    ev.preventDefault();

    const data = new FormData();
    data.set('title', title);
    data.set('summary', summary);
    data.set('content', content);
    if (files?.[0]) {
      data.set('file', files[0]);
    }
    data.set('tags', tags);

    const response = await fetch('http://localhost:4000/post', {
      method: 'POST',
      body: data,
      credentials: 'include'
    });

    if (response.ok) {
      setNotification('Post created successfully!'); // Set notification message
      setTimeout(() => {
        setNotification(''); // Clear notification after 3 seconds
        setRedirect(true);
      }, 3000);
    }
  }

  if (redirect) {
    return <Navigate to="/userlogin" />;
  }

  return (
    <div className="flex items-center justify-center min-h-screen bg-gray-100">
      <form onSubmit={createNewPost} className="flex flex-col gap-4 p-6 bg-white rounded-lg shadow-md w-full max-w-5xl">
        {notification && (
          <div className="mb-4 text-green-600 font-medium">
            {notification}
          </div>
        )}
        <input
          type="text"
          placeholder="Title"
          value={title}
          onChange={ev => setTitle(ev.target.value)}
          className="p-2 border border-gray-300 rounded"
        />
        <input
          type="text"
          placeholder="Summary"
          value={summary}
          onChange={ev => setSummary(ev.target.value)}
          className="p-2 border border-gray-300 rounded"
        />
        <input
          type="file"
          onChange={ev => setFiles(ev.target.files)}
          className="p-2 border border-gray-300 rounded"
        />
        <input
          type="text"
          placeholder="Tags (comma separated)"
          value={tags}
          onChange={ev => setTags(ev.target.value)}
          className="p-2 border border-gray-300 rounded"
        />
        <ReactQuill
          value={content}
          onChange={newValue => setContent(newValue)}
          modules={modules}
          formats={formats}
          className="mt-4 border border-gray-300 rounded"
        />
        <button type="submit" className="mt-4 p-3 bg-blue-500 text-white rounded hover:bg-blue-600">
          Create Post
        </button>
      </form>
    </div>
  );
}
