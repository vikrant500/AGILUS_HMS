import { formatISO9075 } from "date-fns";
import { Link } from "react-router-dom";
import styles from '../../pages/IndexPage/css/CommonStyles.module.css'; // Import the CSS module

export default function Disease({ _id, name, description, createdAt, doctor }) {
  return (
    <div className={styles.post}>
      <div className={styles.image}>
        <Link to={`/disease/${_id}`}>
        </Link>
      </div>
      <div className={styles.texts}>
        <Link to={`/userLogin/disease/${_id}`}>
          <h2>{name}</h2>
        </Link>
        <p className={styles.info}>
          <a className={styles.author}>{doctor.username}</a>
          <time>{formatISO9075(new Date(createdAt))}</time>
        </p>
        <p className={styles.summary}>{description}</p>
      </div>
    </div>
  );
}