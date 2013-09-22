using System;
using System.Security.Cryptography;
using Chimera.Authentication.Shared;
using CuttingEdge.Conditions;


namespace Chimera.Authentication.Core.UserAccounts
{
  public class PasswordHasher
  {
    public static byte[] GeneratePasswordHash(string password, byte[] salt, string hashAlgorithmName = null)
    {
      Condition.Requires(password, "password").IsNotNull();
      Condition.Requires(salt, "salt").IsNotNull();
      hashAlgorithmName = hashAlgorithmName ?? Configuration.Settings.PasswordHashAlgorithm;

      byte[] passwordBytes = System.Text.Encoding.Unicode.GetBytes(password);

      var allBytes = new byte[salt.Length + passwordBytes.Length];

      Buffer.BlockCopy(salt, 0, allBytes, 0, salt.Length);
      Buffer.BlockCopy(passwordBytes, 0, allBytes, salt.Length, passwordBytes.Length);

      HashAlgorithm hasher = HashAlgorithm.Create(hashAlgorithmName);
      if (hasher == null)
        throw new ArgumentException(string.Format("Unknown hashing algorithm '{0}'.", hashAlgorithmName), "hashAlgorithmName");

      return hasher.ComputeHash(allBytes);
    }
  }
}
