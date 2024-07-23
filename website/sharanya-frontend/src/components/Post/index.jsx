import { formatISO9075 } from "date-fns";
import { Link } from "react-router-dom";
import styles from '../../pages/IndexPage/css/CommonStyles.module.css'; // Import the CSS module
import { Buffer } from 'buffer'; // Import Buffer from 'buffer'

export default function Post({ _id, title, summary, cover, content, createdAt, author, tags }) {
  const coverSrc = cover ? `data:image/jpeg;base64,${Buffer.from(cover).toString('base64')}` : '';
  return (
    <div className={styles.post}>
      <div className={styles.image}>
        <Link to={`/userlogin/post/${_id}`}>
        {cover && <img src={coverSrc} alt=""/>}
        </Link>
      </div>
      <div className={styles.texts}>
        <Link to={`/userlogin/post/${_id}`}>
          <h2>{title}</h2>
        </Link>
        <p className={styles.info}>
          <a className={styles.author}>{author.username}</a>
          <time>{formatISO9075(new Date(createdAt))}</time>
        </p>
        <p className={styles.summary}>{summary}</p>
        <div className={styles.tags}>
          {tags && tags.map((tag, index) => (
            <span key={index} className={styles.tag}>{tag}</span>
          ))}
        </div>
      </div>
    </div>
  );
}