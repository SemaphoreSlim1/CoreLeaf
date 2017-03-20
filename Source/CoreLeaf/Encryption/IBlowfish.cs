namespace CoreLeaf.Encryption
{
    public interface IBlowfish
    {
        string ApplyPkcs5Padding(string str);

        string Encrypt_ECB(string pt);

        string Decrypt_ECB(string ct);
    }
}
