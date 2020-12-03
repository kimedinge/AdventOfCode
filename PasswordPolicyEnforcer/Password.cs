namespace PasswordPolicyEnforcer
{
    public class Password
    {
        public int minCharacters { get; set; }
        public int maxCharacters { get; set; }
        public char characterToValidate { get; set; }
        public string passwordToValidate { get; set; }
    }
}