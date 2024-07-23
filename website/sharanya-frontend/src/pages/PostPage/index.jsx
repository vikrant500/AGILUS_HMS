import { useContext, useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { formatISO9075 } from "date-fns";
import styles from './PostPage.module.css';
import { UserContext } from "../../UserContext"; // Correct the import path
import { Link } from 'react-router-dom';
import { Buffer } from 'buffer'; // Import Buffer from 'buffer'
import config from '../../config/config'; // Import the config file

export default function PostPage() {
  const [postInfo, setPostInfo] = useState(null);
  const { userInfo } = useContext(UserContext);
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    fetch(`${config.apiUrl}/post/${id}`)
      .then(response => response.json())
      .then(postInfo => {
        setPostInfo(postInfo);
      });
  }, [id]);

  const deletePost = async () => {
    if (window.confirm("Are you sure you want to delete this post?")) {
      const response = await fetch(`${config.apiUrl}/post/${id}`, {
        method: 'DELETE',
        credentials: 'include',
      });
      if (response.ok) {
        navigate('/userlogin');
      } else {
        console.error('Failed to delete the post', response.statusText);
      }
    }
  };

  if (!postInfo) return '';
  const coverSrc = postInfo.cover ? `data:image/jpeg;base64,${Buffer.from(postInfo.cover).toString('base64')}` : '';

  return (
    <div className={styles.pageWrapper}>
      <div className={styles.postPage}>
        <h1 className={styles.title}>{postInfo.title}</h1>
        {postInfo.author && (
  <div className={styles.author}>by @{postInfo.author.username}</div>
         )
         }
         {userInfo.id === postInfo.author._id && (
        <div className="edit-row flex justify-center mb-4">
        <Link className="bg-gray-300 hover:bg-gray-500 rounded-md text-white px-4 py-2 mr-2" to={`/userlogin/edit/${postInfo._id}`}>
          Edit this post
        </Link>
        <button className="bg-red-300 hover:bg-red-500 rounded-md text-white px-4 py-2 ml-2" onClick={deletePost}>
          Delete Post
        </button>
      </div>
      )}
        <time>{formatISO9075(new Date(postInfo.createdAt))}</time>
        <div className={styles.image}>
          <img src={coverSrc} alt="" />
        </div>
        <div className={styles.tags}>
          {postInfo.tags.map(tag => (
            <span key={tag} className={styles.tag}>{tag}</span>
          ))}
        </div>
        <div className={styles.content} dangerouslySetInnerHTML={{ __html: postInfo.content }} />
      </div>
    </div>
  );
};