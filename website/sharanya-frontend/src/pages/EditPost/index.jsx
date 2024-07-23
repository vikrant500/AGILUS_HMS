import { useEffect, useState } from "react";
import { Navigate, useParams } from "react-router-dom";
import Editor from "../../components/Editor";
import config from '../../config/config'; // Import the config file

export default function EditPost() {
  const { id } = useParams();
  const [title, setTitle] = useState('');
  const [summary, setSummary] = useState('');
  const [content, setContent] = useState('');
  const [files, setFiles] = useState('');
  const [tags, setTags] = useState(''); //new state for tags
  const [redirect, setRedirect] = useState(false);
  const [notification, setNotification] = useState(''); // state for notification message

  useEffect(() => {
    fetch(`${config.apiUrl}/post/${id}`)
      .then(response => {
        response.json().then(postInfo => {
          setTitle(postInfo.title);
          setContent(postInfo.content);
          setSummary(postInfo.summary);
          setTags(postInfo.tags.join(', ')); // Set initial tags
        });
      });
  }, [id]);

  async function updatePost(ev) {
    ev.preventDefault();
    const data = new FormData();
    data.set('title', title);
    data.set('summary', summary);
    data.set('content', content);
    data.set('id', id);
    data.set('tags', tags); // Include tags in form data

    if (files?.[0]) {
      data.set('file', files?.[0]);
    }

    try {
      const response = await fetch(`${config.apiUrl}/post`, {
        method: 'PUT',
        body: data,
        credentials: 'include',
      });
      if (response.ok) {
        setNotification('Post updated successfully!'); // Set notification message
        setTimeout(() => {
          setNotification(''); // Clear notification after 3 seconds
          setRedirect(true);
        }, 3000);
      } else {
        console.error('Failed to update the post', response.statusText);
      }
    } catch (error) {
      console.error('Error during fetch', error);
    }
  }

  if (redirect) {
    return <Navigate to={'/userlogin'} />;
  }

  return (
    <form onSubmit={updatePost} className="max-w-4xl mx-auto bg-white p-12 rounded-lg shadow-lg">
      <h1 className="text-3xl font-bold mb-8">Edit Post</h1>
      {notification && (
        <div className="mb-4 text-green-600 font-medium">
          {notification}
        </div>
      )}
      <div className="mb-6">
        <label className="block text-sm font-medium text-gray-700">Title</label>
        <input
          type="text"
          placeholder="Title"
          value={title}
          onChange={ev => setTitle(ev.target.value)}
          className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
        />
      </div>
      <div className="mb-6">
        <label className="block text-sm font-medium text-gray-700">Summary</label>
        <input
          type="text"
          placeholder="Summary"
          value={summary}
          onChange={ev => setSummary(ev.target.value)}
          className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
        />
      </div>
      <div className="mb-6">
        <label className="block text-sm font-medium text-gray-700">Upload Image</label>
        <input
          type="file"
          onChange={ev => setFiles(ev.target.files)}
          className="mt-1 block w-full text-sm text-gray-500 file:mr-4 file:py-2 file:px-4 file:rounded-md file:border-0 file:text-sm file:font-semibold file:bg-indigo-50 file:text-indigo-500 hover:file:bg-indigo-100"
        />
      </div>
      <div className="mb-6">
        <label className="block text-sm font-medium text-gray-700">Tags</label>
        <input
          type="text"
          placeholder="Tags (comma separated)"
          value={tags}
          onChange={ev => setTags(ev.target.value)}
          className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
        />
      </div>
      <div className="mb-6">
        <label className="block text-sm font-medium text-gray-700">Content</label>
        <Editor onChange={setContent} value={content} />
      </div>
      <button
        type="submit"
        className="w-full py-3 px-6 border border-transparent rounded-md shadow-sm text-lg font-medium text-white bg-blue-500 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-300"
        style={{ marginTop: '5px' }}
      >
        Update post
      </button>
    </form>
  );
}