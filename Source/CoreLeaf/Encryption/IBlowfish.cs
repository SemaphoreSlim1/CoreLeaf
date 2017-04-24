namespace CoreLeaf.Encryption
{
    public interface IBlowfish
    {
        string ApplyPkcs5Padding(string str);

        /// <summary>
        /// Encrypt a string in ECB mode
        /// </summary>
        /// <param name="pt">Plaintext to encrypt as ascii string</param>
        /// <returns>hex value of encrypted data</returns>
        string Encrypt_ECB(string pt);

        /// <summary>
        /// Encrypts a byte array in ECB mode
        /// </summary>
        /// <param name="pt">Plaintext data</param>
        /// <returns>Ciphertext bytes</returns>
        byte[] Encrypt_ECB(byte[] pt);

        /// <summary>
        /// Decrypts a string (ECB)
        /// </summary>
        /// <param name="ct">hHex string of the ciphertext</param>
        /// <returns>Plaintext ascii string</returns>
        string Decrypt_ECB(string ct);

        /// <summary>
        /// Decrypts a byte array (ECB)
        /// </summary>
        /// <param name="ct">Ciphertext byte array</param>
        /// <returns>Plaintext</returns>
        byte[] Decrypt_ECB(byte[] ct);
    }
}
