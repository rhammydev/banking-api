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
}