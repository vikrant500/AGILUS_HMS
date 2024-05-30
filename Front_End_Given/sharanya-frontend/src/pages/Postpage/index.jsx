// src/pages/PostPage/PostPage.jsx
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { formatISO9075 } from "date-fns";
import styles from './PostPage.module.css';

export default function PostPage() {
  const [postInfo, setPostInfo] = useState(null);

  const { id } = useParams();
  useEffect(() => {
    fetch(`http://localhost:4000/post/${id}`)
      .then(response => response.json())
      .then(postInfo => setPostInfo(postInfo));
  }, [id]);

  if (!postInfo) return '';

  return (
    <div className={styles.postPage}>
      <h1>{postInfo.title}</h1>
      <time>{formatISO9075(new Date(postInfo.createdAt))}</time>
      <div className={styles.author}>by @{postInfo.author.username}</div>
      <div className={styles.image}>
        <img src={`http://localhost:4000/${postInfo.cover}`} alt="" />
      </div>
      <div className={styles.content} dangerouslySetInnerHTML={{ __html: postInfo.content }} />
      <div className={styles.tags}>
        {postInfo.tags.map(tag => (
          <span key={tag} className={styles.tag}>{tag}</span>
        ))}
      </div>
    </div>
  );
}
