import React, { useState, useEffect } from 'react';
import { useLocation, Link } from 'react-router-dom';
import { formatISO9075 } from 'date-fns';
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import styles from "./styles/blog.module.css";

export default function IndexPage() {
  const [posts, setPosts] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [searchQuery, setSearchQuery] = useState('');
  const [suggestions, setSuggestions] = useState([]);
  const location = useLocation();

  useEffect(() => {
    const params = new URLSearchParams(location.search);
    const page = parseInt(params.get('page')) || 1;
    const search = params.get('q') || '';
    setCurrentPage(page);
    setSearchQuery(search);
    fetchPosts(page, search);
  }, [location.search]);

  useEffect(() => {
    if (searchQuery) {
      fetch(`http://localhost:4000/suggestions?q=${searchQuery}`)
        .then(response => response.json())
        .then(data => setSuggestions(data));
    } else {
      setSuggestions([]);
    }
  }, [searchQuery]);

  const fetchPosts = async (page, search) => {
    try {
      const response = await fetch(`http://localhost:4000/posts?page=${page}&search=${search}`);
      const data = await response.json();
      setPosts(data.posts);
      setTotalPages(data.totalPages);
    } catch (error) {
      console.error('Error fetching posts:', error);
    }
  };

  const handleSearch = async (e) => {
    e.preventDefault();
    await fetchPosts(1, searchQuery);
    setCurrentPage(1);
    setSuggestions([]);
  };

  const handleSuggestionClick = (suggestion) => {
    setSearchQuery(suggestion.title);
    fetchPosts(1, suggestion.title).then(() => {
      setSuggestions([]);
    });
  };

  const handlePageChange = (page) => {
    fetchPosts(page, searchQuery);
    setCurrentPage(page);
  };

  const renderPageButtons = () => {
    const buttons = [];
    for (let i = 1; i <= totalPages; i++) {
      buttons.push(
        <button
          key={i}
          className={currentPage === i ? styles.selected : ''}
          onClick={() => handlePageChange(i)}
        >
          {i}
        </button>
      );
    }
    return buttons;
  };

  const reloadPage = () => {
    window.location.href = '/blog';
  };

  const handleCategoryClick = (category) => {
    let tag;
    if (category === 'category1') {
      tag = 'game';
    } else if (category === 'category2') {
      tag = 'experiment';
    } else if (category === 'category3') {
      tag = 'wallpaper';
    }
    fetchPosts(1, tag);
  };

  const carouselSettings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 2,
    slidesToScroll: 1,
  };

  return (
    <div className={styles.container}>
      <h1 className={styles.blogTitle}>MyBlog</h1>
      <div className={styles.headerContainer}>
        <div className={styles.categories}>
          <div className={styles.category} onClick={reloadPage}>View All</div>
          <div className={styles.category} onClick={() => handleCategoryClick('category1')}>Category 1</div>
          <div className={styles.category} onClick={() => handleCategoryClick('category2')}>Category 2</div>
          <div className={styles.category} onClick={() => handleCategoryClick('category3')}>Category 3</div>
        </div>
      <form onSubmit={handleSearch} className={`${styles.searchForm} ${styles.headerContainer}`}>
        <input
        type="text"
        placeholder="Search title or tag..."
        value={searchQuery}
        onChange={(e) => setSearchQuery(e.target.value)}
        className={styles.searchBar}
      />
  {suggestions.length > 0 && (
    <ul className={styles.suggestions}>
      {suggestions.map((suggestion, index) => (
        <li
          key={index}
          onClick={() => handleSuggestionClick(suggestion)}
          className={styles.suggestionItem}
        >
          <strong>{suggestion.title}</strong> by {suggestion.author.username}
        </li>
      ))}
    </ul>
  )}
  <button type="submit" className={styles.searchButton}>Search</button>
</form>
</div>
      <div className={styles.posts}>
        {posts.slice(0, 5).map(post => (
          <Post key={post._id} {...post} className={styles.fullWidthPost} />
        ))}
      </div>
      {posts.length > 5 && (
        <div className={styles.carousel}>
          <h1>Featured Posts</h1>
          <Slider {...carouselSettings}>
            {posts.slice(5, 10).map(post => (
              <Post key={post._id} {...post} className={styles.carouselPost} />
            ))}
          </Slider>
        </div>
      )}
      <div className={styles.pagination}>
        <button
          disabled={currentPage === 1}
          onClick={() => handlePageChange(currentPage - 1)}
        >
          Previous
        </button>
        {renderPageButtons()}
        <button
          disabled={currentPage === totalPages}
          onClick={() => handlePageChange(currentPage + 1)}
        >
          Next
        </button>
      </div>
    </div>
  );
}

function Post({ _id, title, summary, cover, createdAt, author, tags, className }) {
  return (
    <div className={`${styles.post} ${className}`}>
      <div className={styles.image}>
        <Link to={`/post/${_id}`}>
          <img src={`http://localhost:4000/${cover}`} alt="" />
        </Link>
      </div>
      <div className={styles.texts}>
      <Link to={`/post/${_id}`}>
         <h2 className={`${styles.title} ${styles.postTitle}`}>{title}</h2>
        </Link>

        <p className={styles.info}>
          <span className={styles.author}>{author.username}</span>
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
