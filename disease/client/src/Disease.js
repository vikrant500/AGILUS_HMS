import {formatISO9075} from "date-fns";
import {Link} from "react-router-dom";

export default function disease({_id,name,description,createdAt,doctor}) {

  return (
    <div className="post">
      <div className="image">
        
      </div>
      <div className="texts">
        <Link to={`/disease/${_id}`}>
        <h2>{name}</h2>
        </Link>
        <p className="info">
          <a className="author">{doctor.username}</a>
          <time>{formatISO9075(new Date(createdAt))}</time>
        </p>
        <p className="summary">{description}</p>
      </div>
    </div>
  );
}