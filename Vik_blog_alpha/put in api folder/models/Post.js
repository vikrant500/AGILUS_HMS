const mongoose = require('mongoose');
const { Schema, model } = mongoose;

const PostSchema = new Schema({
  title: String,
  summary: String,
  content: String,
  cover: String, // -> basically our image
  author: { type: Schema.Types.ObjectId, ref: 'User' },
  tags: [{ type: String }], // New field for tags
}, {
  timestamps: true,
});

const PostModel = model('Post', PostSchema);

module.exports = PostModel;
