import mongoose from 'mongoose';
import bcrypt from 'bcrypt';

const userSchema = new mongoose.Schema({
  _id: mongoose.Schema.Types.ObjectId,
  email: String,
  role: String,
  password: String,
  createdAt: Date,
  permissions: [String],
});

const User = mongoose.model('User', userSchema);

export const populateAdminUser = async () => {
  try {
    await mongoose.connect(process.env.CONNECTION_STRING || '');

    const existingAdmin = await User.findOne({ role: 'admin' }).exec();
    if (existingAdmin) {
      console.log('Admin user already exists.');
      return;
    }

    const hashedPassword = await bcrypt.hash('superadmin', 10);

    const adminUser = new User({
      _id: new mongoose.Types.ObjectId(),
      email: 'admin@email.com',
      role: 'admin',
      password: hashedPassword,
      createdAt: new Date(),
      permissions: [], // Add necessary permissions here
    });

    await adminUser.save();
    console.log('Admin user created successfully.');
  } catch (error) {
    console.error('Error creating admin user:', error);
  }
};
