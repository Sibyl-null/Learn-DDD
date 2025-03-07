﻿using Ardalis.SmartEnum;

namespace DomeGym.Domain.Profiles;

public class ProfileType : SmartEnum<ProfileType>
{
    public static readonly ProfileType Admin = new(nameof(Admin), 0);
    public static readonly ProfileType Trainer = new(nameof(Trainer), 1);
    public static readonly ProfileType Participant = new(nameof(Participant), 2);
    
    private ProfileType(string name, int value) : base(name, value)
    {
    }
}