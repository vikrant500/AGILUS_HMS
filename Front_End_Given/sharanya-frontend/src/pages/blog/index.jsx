import React, { useEffect } from 'react';
import { Container } from '@mui/system';

const Blog = () => {
  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);

  return (
    <div className="relative">
      <div className="py-20 bg-white text-black">
        <h3 className="text-center text-3xl font-bold mb-10">Our Blog</h3>
        <Container>
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
            {/* Sample Blog Post */}
            <div className="bg-gray-100 p-6 rounded-lg shadow-md">
              <h4 className="text-xl font-semibold mb-4">Blog Post Title</h4>
              <p className="text-gray-700 mb-4">This is a sample blog post summary. Click to read more.</p>
              <button className="text-primary hover:text-secondary">Read More</button>
            </div>
            {/* Add more blog posts as needed */}
          </div>
        </Container>
      </div>
    </div>
  );
};

export default Blog;
