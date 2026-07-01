namespace SimpleBankingAPI.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;

public static class MailUtils
{
    private static string GetBaseTemplate(string title, string content)
    {
        return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='utf-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0'/>
    <title>{title}</title>
    <style>
        body {{
            font-family: -apple-system, BlinkMacSystemFont, 'Inter', 'Segoe UI', Roboto, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #F0F4F8;
            color: #1A202C;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }}
        .wrapper {{
            width: 100%;
            table-layout: fixed;
            background: linear-gradient(135deg, #F0F4F8 0%, #E2E8F0 100%);
            padding: 40px 0;
        }}
        .container {{
            max-width: 560px;
            background-color: #FFFFFF;
            margin: 0 auto;
            border-radius: 28px;
            overflow: hidden;
            box-shadow: 0 20px 60px rgba(0, 0, 0, 0.08), 0 8px 24px rgba(0, 0, 0, 0.04);
            border: 1px solid rgba(255, 255, 255, 0.2);
            backdrop-filter: blur(10px);
        }}
        .header {{
            background: linear-gradient(145deg, #0A1628 0%, #1B2B4A 100%);
            padding: 40px 40px 32px;
            text-align: center;
            border-bottom: 4px solid #D4A853;
            position: relative;
            overflow: hidden;
        }}
        .header::before {{
            content: '';
            position: absolute;
            top: -50%;
            right: -20%;
            width: 300px;
            height: 300px;
            background: radial-gradient(circle, rgba(212, 168, 83, 0.08) 0%, transparent 70%);
            border-radius: 50%;
        }}
        .header::after {{
            content: '';
            position: absolute;
            bottom: -40%;
            left: -10%;
            width: 200px;
            height: 200px;
            background: radial-gradient(circle, rgba(212, 168, 83, 0.05) 0%, transparent 70%);
            border-radius: 50%;
        }}
        .logo-mark {{
            margin-bottom: 12px;
            display: inline-block;
            position: relative;
            z-index: 1;
        }}
        .header h1 {{
            color: #FFFFFF;
            margin: 0;
            font-size: 13px;
            font-weight: 600;
            letter-spacing: 4px;
            text-transform: uppercase;
            color: #D4A853;
            position: relative;
            z-index: 1;
        }}
        .header .sub-title {{
            color: #94A3B8;
            font-size: 11px;
            letter-spacing: 2px;
            margin-top: 6px;
            position: relative;
            z-index: 1;
        }}
        .content {{
            padding: 40px 40px 30px;
            background: #FFFFFF;
        }}
        .details-table {{
            width: 100%;
            margin: 24px 0;
            border-collapse: separate;
            border-spacing: 0;
            border-radius: 16px;
            overflow: hidden;
            border: 1px solid #EDF2F7;
        }}
        .details-table th, .details-table td {{
            padding: 14px 20px;
            text-align: left;
            font-size: 13.5px;
        }}
        .details-table tr:not(:last-child) th, 
        .details-table tr:not(:last-child) td {{
            border-bottom: 1px solid #EDF2F7;
        }}
        .details-table th {{
            background-color: #F7FAFC;
            color: #4A5568;
            font-weight: 600;
            width: 40%;
            font-size: 11px;
            letter-spacing: 0.8px;
            text-transform: uppercase;
        }}
        .details-table td {{
            color: #1A202C;
            font-weight: 500;
        }}
        .alert-box {{
            padding: 16px 20px;
            border-radius: 14px;
            margin-bottom: 28px;
            font-size: 13px;
            line-height: 1.6;
            border: 1px solid;
        }}
        .alert-warning {{
            background-color: #FFFBEB;
            border-color: #FDE68A;
            color: #92400E;
        }}
        .alert-danger {{
            background-color: #FEF2F2;
            border-color: #FCA5A5;
            color: #991B1B;
        }}
        .alert-info {{
            background-color: #EFF6FF;
            border-color: #93C5FD;
            color: #1E40AF;
        }}
        .alert-success {{
            background-color: #F0FDF4;
            border-color: #86EFAC;
            color: #166534;
        }}
        .amount-display {{
            font-family: 'SF Mono', 'Menlo', 'Monaco', 'Consolas', monospace;
            font-size: 34px;
            font-weight: 700;
            padding: 16px 28px;
            border-radius: 16px;
            display: inline-block;
            letter-spacing: -0.5px;
        }}
        .amount-debit {{
            color: #DC2626;
            background: #FEF2F2;
            border: 1px solid #FECACA;
        }}
        .amount-credit {{
            color: #059669;
            background: #ECFDF5;
            border: 1px solid #A7F3D0;
        }}
        .status-badge {{
            display: inline-block;
            padding: 4px 14px;
            border-radius: 20px;
            font-size: 11px;
            font-weight: 600;
            letter-spacing: 0.5px;
            text-transform: uppercase;
        }}
        .badge-success {{
            background: #D1FAE5;
            color: #065F46;
            border: 1px solid #A7F3D0;
        }}
        .badge-danger {{
            background: #FEE2E2;
            color: #991B1B;
            border: 1px solid #FCA5A5;
        }}
        .badge-warning {{
            background: #FEF3C7;
            color: #92400E;
            border: 1px solid #FDE68A;
        }}
        .badge-info {{
            background: #DBEAFE;
            color: #1E40AF;
            border: 1px solid #93C5FD;
        }}
        .section-title {{
            color: #1A202C;
            font-size: 13px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1.2px;
            margin: 32px 0 16px 0;
            padding-left: 12px;
            border-left: 3px solid #D4A853;
        }}
        .section-title:first-of-type {{
            margin-top: 0;
        }}
        .footer {{
            background: linear-gradient(145deg, #0A1628 0%, #1B2B4A 100%);
            text-align: center;
            padding: 30px 40px 24px;
            font-size: 11px;
            color: #94A3B8;
            line-height: 1.8;
            letter-spacing: 0.3px;
            border-top: 3px solid #D4A853;
        }}
        .footer strong {{
            color: #FFFFFF;
            font-size: 12px;
            letter-spacing: 1.5px;
        }}
        .footer .footer-divider {{
            width: 40px;
            height: 2px;
            background: #D4A853;
            margin: 12px auto;
            border-radius: 2px;
        }}
        .greeting-text {{
            font-size: 15px;
            color: #4A5568;
            line-height: 1.6;
            margin-bottom: 24px;
        }}
        .transaction-reference {{
            font-family: 'SF Mono', 'Menlo', monospace;
            font-size: 12px;
            color: #718096;
            background: #F7FAFC;
            padding: 2px 10px;
            border-radius: 4px;
            letter-spacing: 0.5px;
        }}
        @media screen and (max-width: 600px) {{
            .wrapper {{ padding: 0; }}
            .container {{ border-radius: 0; border: none; }}
            .content {{ padding: 28px 20px; }}
            .header {{ padding: 32px 20px; }}
            .footer {{ padding: 24px 20px; }}
            .amount-display {{ font-size: 28px; padding: 12px 20px; }}
        }}
    </style>
</head>
<body>
    <div class='wrapper'>
        <div class='container'>
            <div class='header'>
                <div class='logo-mark'>
                    <svg width='48' height='48' viewBox='0 0 48 48' fill='none' xmlns='http://www.w3.org/2000/svg'>
                        <rect x='2' y='2' width='44' height='44' rx='12' stroke='#D4A853' stroke-width='2.5'/>
                        <path d='M14 32V16L24 23L34 16V32' stroke='#FFFFFF' stroke-width='3' stroke-linecap='round' stroke-linejoin='round'/>
                        <circle cx='24' cy='32' r='3' fill='#D4A853'/>
                        <path d='M24 19L24 23' stroke='#D4A853' stroke-width='2' stroke-linecap='round'/>
                    </svg>
                </div>
                <h1>{title}</h1>
                <div class='sub-title'>Prestige Banking Suite • Nigeria</div>
            </div>
            <div class='content'>
                {content}
            </div>
            <div class='footer'>
                <strong>PRESTIGE BANKING</strong>
                <div class='footer-divider'></div>
                &copy; {DateTime.UtcNow.Year} Prestige Banking Services. All rights reserved.<br/>
                <span style='color: #64748B; font-size: 10px; display: inline-block; margin-top: 8px;'>
                    🛡️ This is a system-generated message. Please do not reply directly.
                </span>
            </div>
        </div>
    </div>
</body>
</html>";
    }
    
    public static string GetWelcomeEmailHtml(string customerName, string accountNumber, decimal balance)
    {
        var title = "Welcome to Prestige Banking";
        var content = $@"
            <div style='text-align: center; margin-bottom: 32px;'>
                <h2 style='color: #0A1628; font-size: 24px; font-weight: 700; margin: 0 0 8px 0; letter-spacing: -0.5px;'>
                    Welcome, {customerName}! 👋
                </h2>
                <p style='color: #4A5568; font-size: 15px; margin: 0; line-height: 1.6;'>
                    Your premium banking account has been successfully created.
                </p>
            </div>

            <div style='background: linear-gradient(135deg, #0A1628 0%, #1B2B4A 100%); color: #ffffff; padding: 32px 28px; border-radius: 18px; text-align: center; margin-bottom: 32px; box-shadow: 0 12px 30px rgba(10, 22, 40, 0.2); border: 1px solid rgba(212, 168, 83, 0.2);'>
                <span style='font-size: 10px; text-transform: uppercase; letter-spacing: 3px; color: #D4A853; font-weight: 600; display: block; margin-bottom: 6px;'>
                    Account Number
                </span>
                <h1 style='font-family: ui-monospace, SFMono-Regular, monospace; font-size: 28px; margin: 0 0 12px 0; letter-spacing: 3px; font-weight: 700; color: #FFFFFF;'>
                    {accountNumber}
                </h1>
                <div style='display: inline-block; background: rgba(212, 168, 83, 0.1); border: 1px solid rgba(212, 168, 83, 0.3); padding: 6px 20px; border-radius: 30px; font-size: 14px; font-weight: 600; color: #D4A853;'>
                    ₦ {balance:N2} opening balance
                </div>
            </div>

            <h3 class='section-title'>Account Details</h3>
            <table class='details-table'>
                <tr>
                    <th>Account Holder</th>
                    <td>{customerName}</td>
                </tr>
                <tr>
                    <th>Account Status</th>
                    <td><span class='status-badge badge-success'>● Active</span></td>
                </tr>
                <tr>
                    <th>Currency</th>
                    <td>Nigerian Naira (NGN)</td>
                </tr>
                <tr>
                    <th>Date Opened</th>
                    <td style='font-size: 13px; color: #4A5568;'>{DateTime.UtcNow:dd MMMM yyyy} at {DateTime.UtcNow:HH:mm} (WAT)</td>
                </tr>
            </table>

            <h3 class='section-title'>Getting Started</h3>
            <table width='100%' cellspacing='0' cellpadding='0'>
                <tr>
                    <td style='width: 44px; vertical-align: top; padding-top: 2px;'>
                        <div style='background: linear-gradient(135deg, #0A1628, #1B2B4A); color: #D4A853; border-radius: 10px; width: 28px; height: 28px; line-height: 28px; text-align: center; font-weight: 700; font-size: 12px;'>1</div>
                    </td>
                    <td style='vertical-align: top; padding-left: 12px; padding-bottom: 20px;'>
                        <strong style='color: #0A1628; font-size: 14px; display: block; margin-bottom: 3px;'>Fund Your Account</strong>
                        <span style='color: #4A5568; font-size: 13px; line-height: 1.5;'>Transfer to your account via any bank branch or mobile app using your account number.</span>
                    </td>
                </tr>
                <tr>
                    <td style='width: 44px; vertical-align: top; padding-top: 2px;'>
                        <div style='background: linear-gradient(135deg, #0A1628, #1B2B4A); color: #D4A853; border-radius: 10px; width: 28px; height: 28px; line-height: 28px; text-align: center; font-weight: 700; font-size: 12px;'>2</div>
                    </td>
                    <td style='vertical-align: top; padding-left: 12px; padding-bottom: 20px;'>
                        <strong style='color: #0A1628; font-size: 14px; display: block; margin-bottom: 3px;'>Enable Secure Transactions</strong>
                        <span style='color: #4A5568; font-size: 13px; line-height: 1.5;'>Set up your transaction PIN and biometric authentication for enhanced security.</span>
                    </td>
                </tr>
                <tr>
                    <td style='width: 44px; vertical-align: top; padding-top: 2px;'>
                        <div style='background: linear-gradient(135deg, #0A1628, #1B2B4A); color: #D4A853; border-radius: 10px; width: 28px; height: 28px; line-height: 28px; text-align: center; font-weight: 700; font-size: 12px;'>3</div>
                    </td>
                    <td style='vertical-align: top; padding-left: 12px;'>
                        <strong style='color: #0A1628; font-size: 14px; display: block; margin-bottom: 3px;'>Explore Banking Services</strong>
                        <span style='color: #4A5568; font-size: 13px; line-height: 1.5;'>Access transfers, bill payments, investments, and more through our secure platform.</span>
                    </td>
                </tr>
            </table>

            <div class='alert-box alert-info' style='margin-top: 28px;'>
                <strong>💡 Pro Tip:</strong> Download our mobile app for seamless banking on the go.
            </div>";

        return GetBaseTemplate(title, content);
    }
    
    public static string GetAccountUpdatedEmailHtml(string customerName, string accountNumber, Dictionary<string, string> updatedFields)
    {
        var title = "Account Update Notification";
        
        var fieldsHtml = string.Join("", updatedFields.Select(f => $@"
            <tr>
                <th>{f.Key}</th>
                <td><strong>{f.Value}</strong></td>
            </tr>"));

        var content = $@"
            <div style='text-align: center; margin-bottom: 30px;'>
                <h2 style='color: #0A1628; font-size: 22px; font-weight: 700; margin: 0 0 8px 0;'>
                    Account Details Updated 🔒
                </h2>
                <p style='color: #4A5568; font-size: 15px; margin: 0;'>
                    Dear {customerName}, your account information has been modified.
                </p>
            </div>

            <div class='alert-box alert-warning'>
                <strong>⚠️ Security Notice:</strong> If you did not initiate these changes, please contact our support team immediately at <strong>support@prestigebanking.ng</strong>.
            </div>

            <h3 class='section-title'>Updated Information</h3>
            <table class='details-table'>
                <tr>
                    <th>Account Number</th>
                    <td><strong>{accountNumber}</strong></td>
                </tr>
                {fieldsHtml}
                <tr>
                    <th>Update Time</th>
                    <td style='font-size: 13px; color: #4A5568;'>{DateTime.UtcNow:dd MMMM yyyy} at {DateTime.UtcNow:HH:mm} (WAT)</td>
                </tr>
                <tr>
                    <th>Reference ID</th>
                    <td><span class='transaction-reference'>UPD-{DateTime.UtcNow:yyyyMMdd}-{new Random().Next(1000, 9999)}</span></td>
                </tr>
            </table>

            <div class='alert-box alert-success' style='margin-top: 24px;'>
                <strong>✅ Verification:</strong> You will receive a confirmation SMS on your registered mobile number.
            </div>";

        return GetBaseTemplate(title, content);
    }

    public static string GetAccountDeletedEmailHtml(string customerName, string accountNumber)
    {
        var title = "Account Closure Confirmation";

        var content = $@"
            <div style='text-align: center; margin-bottom: 30px;'>
                <h2 style='color: #0A1628; font-size: 22px; font-weight: 700; margin: 0 0 8px 0;'>
                    Account Successfully Closed 🏦
                </h2>
                <p style='color: #4A5568; font-size: 15px; margin: 0;'>
                    Your account {accountNumber} has been deactivated.
                </p>
            </div>

            <div class='alert-box alert-danger'>
                <strong>⚠️ Important:</strong> This action is irreversible. All associated services and benefits have been terminated.
            </div>

            <h3 class='section-title'>Closure Summary</h3>
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
                    <td><span class='status-badge badge-danger'>● Closed</span></td>
                </tr>
                <tr>
                    <th>Closure Date</th>
                    <td style='font-size: 13px; color: #4A5568;'>{DateTime.UtcNow:dd MMMM yyyy} at {DateTime.UtcNow:HH:mm} (WAT)</td>
                </tr>
            </table>

            <div class='alert-box alert-info' style='margin-top: 24px;'>
                <strong>📞 Need Assistance?</strong> Contact our support team at <strong>support@prestigebanking.ng</strong> or call <strong>+234 700 PRESTIGE</strong>.
            </div>";

        return GetBaseTemplate(title, content);
    }
    
    public static string GetAccountReactivationEmailHtml(string customerName, string accountNumber)
{
    var title = "Account Reactivation Confirmation";
    
    var content = $@"
        <div style='text-align: center; margin-bottom: 30px;'>
            <h2 style='color: #0A1628; font-size: 22px; font-weight: 700; margin: 0 0 8px 0;'>
                Account Successfully Reactivated 🔓
            </h2>
            <p style='color: #4A5568; font-size: 15px; margin: 0;'>
                Dear {customerName}, your account has been restored successfully.
            </p>
        </div>

        <div class='alert-box alert-success'>
            <strong>✅ Account Restored:</strong> Your account is now active and fully operational. All services have been reinstated.
        </div>

        <div style='background: linear-gradient(135deg, #0A1628 0%, #1B2B4A 100%); color: #ffffff; padding: 28px 24px; border-radius: 18px; text-align: center; margin-bottom: 28px; box-shadow: 0 12px 30px rgba(10, 22, 40, 0.2); border: 1px solid rgba(212, 168, 83, 0.2);'>
            <span style='font-size: 10px; text-transform: uppercase; letter-spacing: 3px; color: #D4A853; font-weight: 600; display: block; margin-bottom: 6px;'>
                Reactivated Account
            </span>
            <h1 style='font-family: ui-monospace, SFMono-Regular, monospace; font-size: 26px; margin: 0 0 8px 0; letter-spacing: 3px; font-weight: 700; color: #FFFFFF;'>
                {accountNumber}
            </h1>
            <div style='display: inline-block; background: rgba(212, 168, 83, 0.1); border: 1px solid rgba(212, 168, 83, 0.3); padding: 6px 18px; border-radius: 30px; font-size: 13px; font-weight: 600; color: #D4A853;'>
                <span class='status-badge badge-success' style='background: transparent; color: #D4A853; border-color: #D4A853;'>● Active</span>
            </div>
        </div>

        <h3 class='section-title'>Reactivation Summary</h3>
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
                <th>Account Status</th>
                <td><span class='status-badge badge-success'>● Active</span></td>
            </tr>
            <tr>
                <th>Reactivation Date</th>
                <td style='font-size: 13px; color: #4A5568;'>{DateTime.UtcNow:dd MMMM yyyy} at {DateTime.UtcNow:HH:mm} (WAT)</td>
            </tr>
            <tr>
                <th>Reference ID</th>
                <td><span class='transaction-reference'>REACT-{DateTime.UtcNow:yyyyMMdd}-{new Random().Next(1000, 9999)}</span></td>
            </tr>
        </table>

        <h3 class='section-title'>What Happens Next</h3>
        <table width='100%' cellspacing='0' cellpadding='0'>
            <tr>
                <td style='width: 44px; vertical-align: top; padding-top: 2px;'>
                    <div style='background: linear-gradient(135deg, #0A1628, #1B2B4A); color: #D4A853; border-radius: 10px; width: 28px; height: 28px; line-height: 28px; text-align: center; font-weight: 700; font-size: 12px;'>1</div>
                </td>
                <td style='vertical-align: top; padding-left: 12px; padding-bottom: 20px;'>
                    <strong style='color: #0A1628; font-size: 14px; display: block; margin-bottom: 3px;'>Access Restored</strong>
                    <span style='color: #4A5568; font-size: 13px; line-height: 1.5;'>You can now perform all banking transactions including transfers, withdrawals, and deposits.</span>
                </td>
            </tr>
            <tr>
                <td style='width: 44px; vertical-align: top; padding-top: 2px;'>
                    <div style='background: linear-gradient(135deg, #0A1628, #1B2B4A); color: #D4A853; border-radius: 10px; width: 28px; height: 28px; line-height: 28px; text-align: center; font-weight: 700; font-size: 12px;'>2</div>
                </td>
                <td style='vertical-align: top; padding-left: 12px; padding-bottom: 20px;'>
                    <strong style='color: #0A1628; font-size: 14px; display: block; margin-bottom: 3px;'>Security Verification</strong>
                    <span style='color: #4A5568; font-size: 13px; line-height: 1.5;'>Your security settings remain intact. For enhanced protection, consider updating your PIN and passwords.</span>
                </td>
            </tr>
            <tr>
                <td style='width: 44px; vertical-align: top; padding-top: 2px;'>
                    <div style='background: linear-gradient(135deg, #0A1628, #1B2B4A); color: #D4A853; border-radius: 10px; width: 28px; height: 28px; line-height: 28px; text-align: center; font-weight: 700; font-size: 12px;'>3</div>
                </td>
                <td style='vertical-align: top; padding-left: 12px;'>
                    <strong style='color: #0A1628; font-size: 14px; display: block; margin-bottom: 3px;'>Stay Informed</strong>
                    <span style='color: #4A5568; font-size: 13px; line-height: 1.5;'>Monitor your account regularly for any suspicious activities. We'll notify you of all transactions.</span>
                </td>
            </tr>
        </table>

        <div class='alert-box alert-info' style='margin-top: 28px;'>
            <strong>📱 Need Help?</strong> Download our mobile app for 24/7 account access or call our support team at <strong>+234 700 PRESTIGE</strong>.
        </div>";

    return GetBaseTemplate(title, content);
}
    public static string GetTransferDebitEmailHtml(string customerName, string accountNumber, string recipientAccountNumber, decimal amount, decimal newBalance)
    {
        var title = "Debit Transaction Alert";

        var content = $@"
            <div style='text-align: center; margin-bottom: 30px;'>
                <h2 style='color: #0A1628; font-size: 22px; font-weight: 700; margin: 0 0 8px 0;'>
                    Fund Transfer Completed 💸
                </h2>
                <p style='color: #4A5568; font-size: 15px; margin: 0;'>
                    Hi {customerName}, your transfer has been processed.
                </p>
            </div>

            <div style='text-align: center; margin-bottom: 32px;'>
                <div class='amount-display amount-debit'>
                    - ₦ {amount:N2}
                </div>
            </div>

            <h3 class='section-title'>Transaction Details</h3>
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
                    <th>Amount</th>
                    <td style='color: #DC2626; font-weight: 700;'>₦ {amount:N2}</td>
                </tr>
                <tr>
                    <th>New Balance</th>
                    <td style='color: #0A1628; font-weight: 700;'>₦ {newBalance:N2}</td>
                </tr>
                <tr>
                    <th>Transaction Time</th>
                    <td style='font-size: 13px; color: #4A5568;'>{DateTime.UtcNow:dd MMMM yyyy} at {DateTime.UtcNow:HH:mm} (WAT)</td>
                </tr>
                <tr>
                    <th>Reference</th>
                    <td><span class='transaction-reference'>TXN-{DateTime.UtcNow:yyyyMMdd}-{new Random().Next(10000, 99999)}</span></td>
                </tr>
            </table>

            <div class='alert-box alert-warning'>
                <strong>🛡️ Security Check:</strong> If you didn't authorize this transaction, please call us immediately at <strong>+234 700 PRESTIGE</strong>.
            </div>";

        return GetBaseTemplate(title, content);
    }
    
    public static string GetTransferCreditEmailHtml(string customerName, string accountNumber, string senderAccountNumber, decimal amount, decimal newBalance)
    {
        var title = "Credit Transaction Alert";

        var content = $@"
            <div style='text-align: center; margin-bottom: 30px;'>
                <h2 style='color: #0A1628; font-size: 22px; font-weight: 700; margin: 0 0 8px 0;'>
                    Funds Received 💰
                </h2>
                <p style='color: #4A5568; font-size: 15px; margin: 0;'>
                    Hi {customerName}, you've received a credit into your account.
                </p>
            </div>

            <div style='text-align: center; margin-bottom: 32px;'>
                <div class='amount-display amount-credit'>
                    + ₦ {amount:N2}
                </div>
            </div>

            <h3 class='section-title'>Transaction Details</h3>
            <table class='details-table'>
                <tr>
                    <th>From Account</th>
                    <td>{senderAccountNumber}</td>
                </tr>
                <tr>
                    <th>To Account</th>
                    <td>{accountNumber}</td>
                </tr>
                <tr>
                    <th>Amount</th>
                    <td style='color: #059669; font-weight: 700;'>₦ {amount:N2}</td>
                </tr>
                <tr>
                    <th>New Balance</th>
                    <td style='color: #0A1628; font-weight: 700;'>₦ {newBalance:N2}</td>
                </tr>
                <tr>
                    <th>Transaction Time</th>
                    <td style='font-size: 13px; color: #4A5568;'>{DateTime.UtcNow:dd MMMM yyyy} at {DateTime.UtcNow:HH:mm} (WAT)</td>
                </tr>
                <tr>
                    <th>Reference</th>
                    <td><span class='transaction-reference'>TXN-{DateTime.UtcNow:yyyyMMdd}-{new Random().Next(10000, 99999)}</span></td>
                </tr>
            </table>

            <div class='alert-box alert-success'>
                <strong>✅ Confirmation:</strong> Your account has been credited successfully.
            </div>";

        return GetBaseTemplate(title, content);
    }

    public static string GetDepositEmailHtml(string customerName, string accountNumber, decimal amount, decimal newBalance)
    {
        var title = "Deposit Confirmation";

        var content = $@"
            <div style='text-align: center; margin-bottom: 30px;'>
                <h2 style='color: #0A1628; font-size: 22px; font-weight: 700; margin: 0 0 8px 0;'>
                    Deposit Successful 💳
                </h2>
                <p style='color: #4A5568; font-size: 15px; margin: 0;'>
                    Hi {customerName}, your deposit has been confirmed.
                </p>
            </div>

            <div style='text-align: center; margin-bottom: 32px;'>
                <div class='amount-display amount-credit'>
                    + ₦ {amount:N2}
                </div>
            </div>

            <h3 class='section-title'>Deposit Details</h3>
            <table class='details-table'>
                <tr>
                    <th>Account</th>
                    <td>{accountNumber}</td>
                </tr>
                <tr>
                    <th>Deposit Amount</th>
                    <td style='color: #059669; font-weight: 700;'>₦ {amount:N2}</td>
                </tr>
                <tr>
                    <th>New Balance</th>
                    <td style='color: #0A1628; font-weight: 700;'>₦ {newBalance:N2}</td>
                </tr>
                <tr>
                    <th>Transaction Time</th>
                    <td style='font-size: 13px; color: #4A5568;'>{DateTime.UtcNow:dd MMMM yyyy} at {DateTime.UtcNow:HH:mm} (WAT)</td>
                </tr>
                <tr>
                    <th>Reference</th>
                    <td><span class='transaction-reference'>DEP-{DateTime.UtcNow:yyyyMMdd}-{new Random().Next(10000, 99999)}</span></td>
                </tr>
            </table>

            <div class='alert-box alert-success'>
                <strong>✅ Confirmation:</strong> Your deposit has been posted to your account.
            </div>";

        return GetBaseTemplate(title, content);
    }

    public static string GetWithdrawalEmailHtml(string customerName, string accountNumber, decimal amount, decimal newBalance)
    {
        var title = "Withdrawal Confirmation";

        var content = $@"
            <div style='text-align: center; margin-bottom: 30px;'>
                <h2 style='color: #0A1628; font-size: 22px; font-weight: 700; margin: 0 0 8px 0;'>
                    Withdrawal Processed 💵
                </h2>
                <p style='color: #4A5568; font-size: 15px; margin: 0;'>
                    Hi {customerName}, your withdrawal has been completed.
                </p>
            </div>

            <div style='text-align: center; margin-bottom: 32px;'>
                <div class='amount-display amount-debit'>
                    - ₦ {amount:N2}
                </div>
            </div>

            <h3 class='section-title'>Withdrawal Details</h3>
            <table class='details-table'>
                <tr>
                    <th>Account</th>
                    <td>{accountNumber}</td>
                </tr>
                <tr>
                    <th>Withdrawal Amount</th>
                    <td style='color: #DC2626; font-weight: 700;'>₦ {amount:N2}</td>
                </tr>
                <tr>
                    <th>New Balance</th>
                    <td style='color: #0A1628; font-weight: 700;'>₦ {newBalance:N2}</td>
                </tr>
                <tr>
                    <th>Transaction Time</th>
                    <td style='font-size: 13px; color: #4A5568;'>{DateTime.UtcNow:dd MMMM yyyy} at {DateTime.UtcNow:HH:mm} (WAT)</td>
                </tr>
                <tr>
                    <th>Reference</th>
                    <td><span class='transaction-reference'>WTH-{DateTime.UtcNow:yyyyMMdd}-{new Random().Next(10000, 99999)}</span></td>
                </tr>
            </table>

            <div class='alert-box alert-warning'>
                <strong>🛡️ Security Check:</strong> If you didn't authorize this withdrawal, contact us immediately at <strong>+234 700 PRESTIGE</strong>.
            </div>";

        return GetBaseTemplate(title, content);
    }
}