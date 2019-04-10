﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp06.Models.Test;

namespace WebApp06.Models.Role
{
	public interface IRoleService
	{
		string AddSubjectToProf(string prof, int subject);

		List<Professor> GetProfessors();
	}
}
