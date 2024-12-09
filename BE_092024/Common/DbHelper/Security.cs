using System.Security.Cryptography;
using System.Text;

namespace Common.DbHelper
{
    public static class Security
    {
        // Phương thức mã hóa mật khẩu với SHA256 và Salt
        public static string HashPassword(string password)
        {
            // Tạo salt ngẫu nhiên
            byte[] salt = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Kết hợp mật khẩu và salt
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] passwordWithSalt = new byte[passwordBytes.Length + salt.Length];
            Buffer.BlockCopy(passwordBytes, 0, passwordWithSalt, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, passwordWithSalt, passwordBytes.Length, salt.Length);

            // Tính băm SHA256 của mật khẩu kết hợp với salt
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(passwordWithSalt);

                // Chuyển kết quả băm thành chuỗi hex
                string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                // Trả về chuỗi hash và salt đã kết hợp
                return hash + ":" + Convert.ToBase64String(salt);
            }
        }

        // Phương thức xác thực mật khẩu (so sánh mật khẩu người dùng nhập với mật khẩu đã mã hóa)
        public static bool VerifyPassword(string enteredPassword, string storedHashedPasswordWithSalt)
        {
            // Tách hash và salt từ mật khẩu đã lưu
            var parts = storedHashedPasswordWithSalt.Split(':');
            if (parts.Length != 2) return false;

            string storedHash = parts[0];
            byte[] salt = Convert.FromBase64String(parts[1]);

            // Kết hợp mật khẩu nhập vào với salt lưu trữ
            byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);
            byte[] enteredPasswordWithSalt = new byte[enteredPasswordBytes.Length + salt.Length];
            Buffer.BlockCopy(enteredPasswordBytes, 0, enteredPasswordWithSalt, 0, enteredPasswordBytes.Length);
            Buffer.BlockCopy(salt, 0, enteredPasswordWithSalt, enteredPasswordBytes.Length, salt.Length);

            // Tính toán băm SHA256 của mật khẩu nhập vào với salt
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(enteredPasswordWithSalt);
                string enteredHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                // So sánh băm của mật khẩu nhập vào với băm đã lưu
                return enteredHash == storedHash;
            }
        }
    }
}
