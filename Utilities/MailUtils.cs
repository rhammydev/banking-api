namespace SimpleBankingAPI.Utilities;

public static class MailUtils
{
    public static string GetEmailWrapper(string title, string content)
    {
                 return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8' />
    <title>{title}</title>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f6f9;
            color: #333333;
        }}
        .container {{
            max-width: 600px;
            margin: 40px auto;
            background: #ffffff;
            border-radius: 12px;
            overflow: hidden;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.05);
            border: 1px solid #e1e8ed;
        }}
        .header {{
            background: linear-gradient(135deg, #1e3c72 0%, #2a5298 100%);
            color: #ffffff;
            padding: 30px;
            text-align: center;
        }}
        .header h1 {{
            margin: 0;
            font-size: 24px;
            font-weight: 600;
            letter-spacing: 1px;
        }}
        .content {{
            padding: 40px 30px;
            line-height: 1.6;
        }}
        .content h2 {{
            color: #1e3c72;
            margin-top: 0;
            font-size: 20px;
        }}
        .details-table {{
            width: 100%;
            margin: 25px 0;
            border-collapse: collapse;
        }}
        .details-table th, .details-table td {{
            padding: 12px 15px;
            text-align: left;
            border-bottom: 1px solid #eef2f5;
        }}
        .details-table th {{
            background-color: #f8fafc;
            color: #4a5568;
            font-weight: 600;
            width: 40%;
        }}
        .details-table td {{
            color: #2d3748;
        }}
        .amount-highlight {{
            font-size: 22px;
            font-weight: bold;
            color: #2b6cb0;
            background-color: #ebf8ff;
            padding: 8px 15px;
            border-radius: 6px;
            display: inline-block;
            margin: 10px 0;
        }}
        .footer {{
            background-color: #f8fafc;
            text-align: center;
            padding: 20px;
            font-size: 12px;
            color: #718096;
            border-top: 1px solid #eef2f5;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>{title}</h1>
        </div>
        <div class='content'>
            {content}
        </div>
        <div class='footer'>
            &copy; {DateTime.UtcNow.Year} Simple Banking API. All rights reserved.<br/>
            This is a secure system notification. Please do not reply directly to this email.
        </div>
    </div>
</body>
</html>";
    }
    
    public static string GetWelcomeEmailHtml(string customerName, string accountNumber, decimal balance)
    {
        var title = "Welcome to Simple Banking API!";
            var content = $@"
                <div style='text-align: center; margin-bottom: 30px;'>
                    <h2 style='color: #1e3c72; font-size: 24px; margin-bottom: 10px;'>Welcome to the Future of Banking, {customerName}!</h2>
                    <p style='color: #4a5568; font-size: 16px; margin: 0;'>We are thrilled to have you with us. Your account is fully active and ready for use.</p>
                </div>
                
                <div style='background: linear-gradient(135deg, #1e3c72 0%, #2a5298 100%); color: #ffffff; padding: 25px; border-radius: 10px; text-align: center; margin-bottom: 35px; box-shadow: 0 4px 10px rgba(30, 60, 114, 0.15);'>
                    <span style='font-size: 13px; text-transform: uppercase; letter-spacing: 1.5px; opacity: 0.85;'>Your New Account Number</span>
                    <h1 style='font-family: Courier, monospace; font-size: 32px; margin: 8px 0; letter-spacing: 4px; font-weight: 700;'>{accountNumber}</h1>
                    <span style='font-size: 14px; opacity: 0.9;'>Opening Balance: <strong>₦{balance:N2}</strong></span>
                </div>

                <h3 style='color: #2d3748; border-bottom: 2px solid #eef2f5; padding-bottom: 8px; margin-bottom: 15px;'>Account Information</h3>
                <table class='details-table'>
                    <tr>
                        <th>Account Holder</th>
                        <td>{customerName}</td>
                    </tr>
                    <tr>
                        <th>Account Status</th>
                        <td><span style='color: #2f855a; background-color: #f0fff4; padding: 4px 10px; border-radius: 9999px; font-size: 12px; font-weight: 600;'>ACTIVE</span></td>
                    </tr>
                    <tr>
                        <th>Currency</th>
                        <td>Nigerian Naira (₦)</td>
                    </tr>
                    <tr>
                        <th>Date Opened</th>
                        <td>{DateTime.UtcNow:f} (UTC)</td>
                    </tr>
                </table>

                <h3 style='color: #2d3748; border-bottom: 2px solid #eef2f5; padding-bottom: 8px; margin-top: 30px; margin-bottom: 15px;'>Quick Onboarding Steps</h3>
                <div style='margin-bottom: 10px;'>
                    <div style='display: flex; align-items: flex-start; margin-bottom: 15px;'>
                        <div style='background-color: #ebf8ff; color: #2b6cb0; border-radius: 50%; width: 28px; height: 28px; line-height: 28px; text-align: center; font-weight: bold; margin-right: 12px; flex-shrink: 0;'>1</div>
                        <div>
                            <strong style='color: #2d3748;'>Fund Your Account</strong>
                            <p style='margin: 3px 0 0 0; color: #4a5568; font-size: 14px;'>Deposit money using our secure deposit endpoint to get started.</p>
                        </div>
                    </div>
                    <div style='display: flex; align-items: flex-start; margin-bottom: 15px;'>
                        <div style='background-color: #ebf8ff; color: #2b6cb0; border-radius: 50%; width: 28px; height: 28px; line-height: 28px; text-align: center; font-weight: bold; margin-right: 12px; flex-shrink: 0;'>2</div>
                        <div>
                            <strong style='color: #2d3748;'>Transfer Instantly</strong>
                            <p style='margin: 3px 0 0 0; color: #4a5568; font-size: 14px;'>Make free, instant transfers to other accounts inside the network.</p>
                        </div>
                    </div>
                    <div style='display: flex; align-items: flex-start; margin-bottom: 15px;'>
                        <div style='background-color: #ebf8ff; color: #2b6cb0; border-radius: 50%; width: 28px; height: 28px; line-height: 28px; text-align: center; font-weight: bold; margin-right: 12px; flex-shrink: 0;'>3</div>
                        <div>
                            <strong style='color: #2d3748;'>Track Transactions</strong>
                            <p style='margin: 3px 0 0 0; color: #4a5568; font-size: 14px;'>Retrieve your complete, real-time transaction history at any time.</p>
                        </div>
                    </div>
                </div>";

            return GetEmailWrapper(title, content);
    }
    
    public static string GetAccountUpdatedEmailHtml(string customerName, string accountNumber, Dictionary<string, string> updatedFields)
{
    var title = "Account Details Updated";
    
    var fieldsHtml = string.Join("", updatedFields.Select(f => $@"
        <tr>
            <th>{f.Key}</th>
            <td>{f.Value}</td>
        </tr>"));

    var content = $@"
        <div style='text-align: center; margin-bottom: 30px;'>
            <h2 style='color: #1e3c72; font-size: 24px; margin-bottom: 10px;'>Account Update Notification</h2>
            <p style='color: #4a5568; font-size: 16px; margin: 0;'>Hi {customerName}, your account details have been successfully updated.</p>
        </div>

        <div style='background-color: #fffbeb; border-left: 4px solid #f6ad55; padding: 15px 20px; border-radius: 6px; margin-bottom: 25px;'>
            <p style='margin: 0; color: #744210; font-size: 14px;'>
                ⚠️ If you did not request this change, please contact support immediately.
            </p>
        </div>

        <h3 style='color: #2d3748; border-bottom: 2px solid #eef2f5; padding-bottom: 8px; margin-bottom: 15px;'>Updated Information</h3>
        <table class='details-table'>
            <tr>
                <th>Account Number</th>
                <td>{accountNumber}</td>
            </tr>
            {fieldsHtml}
            <tr>
                <th>Updated At</th>
                <td>{DateTime.UtcNow:f} (UTC)</td>
            </tr>
        </table>

        <p style='color: #4a5568; font-size: 14px; margin-top: 25px;'>
            If this was you, no further action is needed. Your account remains active and secure.
        </p>";

    return GetEmailWrapper(title, content);
}

    public static string GetAccountDeletedEmailHtml(string customerName, string accountNumber)
{
    var title = "Account Closed";

    var content = $@"
        <div style='text-align: center; margin-bottom: 30px;'>
            <h2 style='color: #1e3c72; font-size: 24px; margin-bottom: 10px;'>We're Sorry to See You Go, {customerName}</h2>
            <p style='color: #4a5568; font-size: 16px; margin: 0;'>Your account has been successfully closed as requested.</p>
        </div>

        <div style='background-color: #fff5f5; border-left: 4px solid #fc8181; padding: 15px 20px; border-radius: 6px; margin-bottom: 25px;'>
            <p style='margin: 0; color: #742a2a; font-size: 14px;'>
                ⚠️ If you did not request this closure, please contact support immediately as this action may be reversible within 24 hours.
            </p>
        </div>

        <h3 style='color: #2d3748; border-bottom: 2px solid #eef2f5; padding-bottom: 8px; margin-bottom: 15px;'>Closure Summary</h3>
        <table class='details-table'>
            <tr>
                <th>Account Holder</th>
                <td>{customerName}</td>
            </tr>
            <tr>
                <th>Account Number</th>
                <td>{accountNumber}</td>
            </tr>
            <tr>
                <th>Status</th>
                <td><span style='color: #c53030; background-color: #fff5f5; padding: 4px 10px; border-radius: 9999px; font-size: 12px; font-weight: 600;'>CLOSED</span></td>
            </tr>
            <tr>
                <th>Closed At</th>
                <td>{DateTime.UtcNow:f} (UTC)</td>
            </tr>
        </table>

        <p style='color: #4a5568; font-size: 14px; margin-top: 25px;'>
            Thank you for banking with us. We hope to serve you again in the future.
        </p>";

    return GetEmailWrapper(title, content);
}
    
    public static string GetTransferEmailHtml(string customerName, string accountNumber, string recipientAccountNumber, decimal amount, decimal newBalance)
{
    var title = "Transfer Successful";

    var content = $@"
        <div style='text-align: center; margin-bottom: 30px;'>
            <h2 style='color: #1e3c72; font-size: 24px; margin-bottom: 10px;'>Transfer Confirmation</h2>
            <p style='color: #4a5568; font-size: 16px; margin: 0;'>Hi {customerName}, your transfer was completed successfully.</p>
        </div>

        <div style='text-align: center; margin-bottom: 30px;'>
            <span class='amount-highlight'>- ₦{amount:N2}</span>
        </div>

        <h3 style='color: #2d3748; border-bottom: 2px solid #eef2f5; padding-bottom: 8px; margin-bottom: 15px;'>Transfer Details</h3>
        <table class='details-table'>
            <tr>
                <th>From Account</th>
                <td>{accountNumber}</td>
            </tr>
            <tr>
                <th>To Account</th>
                <td>{recipientAccountNumber}</td>
            </tr>
            <tr>
                <th>Amount Sent</th>
                <td>₦{amount:N2}</td>
            </tr>
            <tr>
                <th>New Balance</th>
                <td>₦{newBalance:N2}</td>
            </tr>
            <tr>
                <th>Date & Time</th>
                <td>{DateTime.UtcNow:f} (UTC)</td>
            </tr>
        </table>

        <div style='background-color: #fffbeb; border-left: 4px solid #f6ad55; padding: 15px 20px; border-radius: 6px; margin-top: 25px;'>
            <p style='margin: 0; color: #744210; font-size: 14px;'>
                ⚠️ If you did not initiate this transfer, please contact support immediately.
            </p>
        </div>";

    return GetEmailWrapper(title, content);
}

public static string GetDepositEmailHtml(string customerName, string accountNumber, decimal amount, decimal newBalance)
{
    var title = "Deposit Successful";

    var content = $@"
        <div style='text-align: center; margin-bottom: 30px;'>
            <h2 style='color: #1e3c72; font-size: 24px; margin-bottom: 10px;'>Deposit Confirmation</h2>
            <p style='color: #4a5568; font-size: 16px; margin: 0;'>Hi {customerName}, your deposit was received successfully.</p>
        </div>

        <div style='text-align: center; margin-bottom: 30px;'>
            <span class='amount-highlight' style='color: #2f855a; background-color: #f0fff4;'>+ ₦{amount:N2}</span>
        </div>

        <h3 style='color: #2d3748; border-bottom: 2px solid #eef2f5; padding-bottom: 8px; margin-bottom: 15px;'>Deposit Details</h3>
        <table class='details-table'>
            <tr>
                <th>Account Number</th>
                <td>{accountNumber}</td>
            </tr>
            <tr>
                <th>Account Holder</th>
                <td>{customerName}</td>
            </tr>
            <tr>
                <th>Amount Deposited</th>
                <td>₦{amount:N2}</td>
            </tr>
            <tr>
                <th>New Balance</th>
                <td>₦{newBalance:N2}</td>
            </tr>
            <tr>
                <th>Date & Time</th>
                <td>{DateTime.UtcNow:f} (UTC)</td>
            </tr>
        </table>

        <p style='color: #4a5568; font-size: 14px; margin-top: 25px;'>
            Your funds are available immediately. Thank you for banking with us.
        </p>";

    return GetEmailWrapper(title, content);
}

public static string GetWithdrawalEmailHtml(string customerName, string accountNumber, decimal amount, decimal newBalance)
{
    var title = "Withdrawal Successful";

    var content = $@"
        <div style='text-align: center; margin-bottom: 30px;'>
            <h2 style='color: #1e3c72; font-size: 24px; margin-bottom: 10px;'>Withdrawal Confirmation</h2>
            <p style='color: #4a5568; font-size: 16px; margin: 0;'>Hi {customerName}, your withdrawal was processed successfully.</p>
        </div>

        <div style='text-align: center; margin-bottom: 30px;'>
            <span class='amount-highlight'>- ₦{amount:N2}</span>
        </div>

        <h3 style='color: #2d3748; border-bottom: 2px solid #eef2f5; padding-bottom: 8px; margin-bottom: 15px;'>Withdrawal Details</h3>
        <table class='details-table'>
            <tr>
                <th>Account Number</th>
                <td>{accountNumber}</td>
            </tr>
            <tr>
                <th>Account Holder</th>
                <td>{customerName}</td>
            </tr>
            <tr>
                <th>Amount Withdrawn</th>
                <td>₦{amount:N2}</td>
            </tr>
            <tr>
                <th>New Balance</th>
                <td>₦{newBalance:N2}</td>
            </tr>
            <tr>
                <th>Date & Time</th>
                <td>{DateTime.UtcNow:f} (UTC)</td>
            </tr>
        </table>

        <div style='background-color: #fffbeb; border-left: 4px solid #f6ad55; padding: 15px 20px; border-radius: 6px; margin-top: 25px;'>
            <p style='margin: 0; color: #744210; font-size: 14px;'>
                ⚠️ If you did not initiate this withdrawal, please contact support immediately.
            </p>
        </div>";

    return GetEmailWrapper(title, content);
}
}