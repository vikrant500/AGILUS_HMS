import React from 'react';
import styles from './styles/blog.module.css';

export default function Sidebar({ tags, handleTagClick }) {
  return (
    <div className={styles.sidebar}>
      <h1>Tags</h1>
      <hr className={styles.sidebarDivider} /> {/* Horizontal line */}
      <ul className={styles.tagList}>
        {tags.map((tag, index) => (
          <li key={index} className={styles.tagItem}>
            <a href="#" onClick={() => handleTagClick(tag)}>{tag}</a>
          </li>
        ))}
      </ul>
    </div>
  );
}
