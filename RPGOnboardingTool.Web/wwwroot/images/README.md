# Image Organization Guide

This directory contains all images used in the RPG Onboarding Tool application.

## Directory Structure

```
images/
├── avatars/          # Character avatar images
├── equipment/        # Equipment and weapon images  
├── races/           # Race/species images
├── placeholder.png           # Generic placeholder
└── placeholder-equipment.png # Equipment placeholder
```

## Image Naming Conventions

### Race Images (`/races/`)
- Format: `{race-name}.png`
- Examples:
  - `human.png`
  - `ebon.png` 
  - `frother.png`
  - `neophron.png`
  - `shaktar.png`
  - `stormer-malice.png`
  - `stormer-xeno.png`
  - `wraithen.png`

### Equipment Images (`/equipment/`)
- Format: `{equipment-name}.png` or `{equipment-id}.png`
- Use lowercase with hyphens for spaces
- Examples:
  - `combat-knife.png`
  - `las-rifle.png`
  - `heavy-armor.png`

### Avatar Images (`/avatars/`)
- Format: `{character-id}_{random-filename}.{extension}`
- Generated automatically when avatars are uploaded
- Examples:
  - `123e4567-e89b-12d3-a456-426614174000_abc123.png`

## Recommended Image Specifications

### Race Images
- **Size**: 500x375px (4:3 aspect ratio) for optimal display
- **Minimum Size**: 400x300px (4:3 aspect ratio)
- **Format**: PNG with transparency preferred
- **File Size**: < 300KB

### Equipment Images  
- **Size**: 128x128px (square)
- **Format**: PNG with transparency
- **File Size**: < 100KB

### Avatar Images
- **Size**: 200x200px (square)
- **Format**: PNG or JPG
- **File Size**: < 500KB

## Usage in Code

### Race Images
```javascript
// In site.js - Race selection
raceImage.src = `/images/races/${selectedRace.name.toLowerCase().replace(/\s/g, '-')}.png`;
```

### Equipment Images
```csharp
// In database seeding or equipment creation
EquipmentItem.ImageUrl = "/images/equipment/combat-knife.png";
```

### Avatar Images
```csharp
// In CharacterService.cs - Avatar upload
character.AvatarUrl = $"/images/avatars/{fileName}";
```

## Adding New Images

1. **Race Images**: Place in `/races/` folder with the race name (lowercase, hyphens for spaces)
2. **Equipment Images**: Place in `/equipment/` folder with descriptive filename
3. **Avatar Images**: Uploaded automatically through the character edit interface

## File Formats Supported

- **PNG** (preferred for transparency)
- **JPG/JPEG** (for photographs)
- **WebP** (modern format, smaller file sizes)

## Notes

- All image paths are relative to `/wwwroot/images/`
- Images are served statically by ASP.NET Core
- Placeholder images are used when specific images are not found
- The `.gitkeep` files ensure empty directories are tracked in version control