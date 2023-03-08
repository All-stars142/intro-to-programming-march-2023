using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status; 
public record StatusMessage(Guid Id, string CurrentMessage, DateTimeOffset When);
public record StatusChangeRequest(string Message);