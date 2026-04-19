#!/usr/bin/env node
// Bumps <Version> (or <FileVersion>) in the main .csproj.
// Never touches <AssemblyVersion>.
// Usage: node tools/version-bump.js <newVersion>

const fs = require('fs');
const path = require('path');

const newVersion = process.argv[2];
if (!newVersion) {
  console.error('Usage: node tools/version-bump.js <version>');
  process.exit(1);
}

function findCsproj(dir) {
  const results = [];
  let entries;
  try { entries = fs.readdirSync(dir, { withFileTypes: true }); }
  catch { return results; }
  for (const entry of entries) {
    const full = path.join(dir, entry.name);
    if (entry.isDirectory()) {
      if (entry.name === 'obj' || entry.name === 'bin' || entry.name === 'node_modules' || entry.name === '.git') continue;
      results.push(...findCsproj(full));
    } else if (entry.name.endsWith('.csproj') && !entry.name.endsWith('.Tests.csproj')) {
      results.push(full);
    }
  }
  return results;
}

const repoRoot = path.resolve(__dirname, '..');
const files = findCsproj(repoRoot);

if (files.length === 0) {
  console.error('No .csproj found');
  process.exit(1);
}

// Pick the main project (prefer the one matching the repo folder name)
const repoName = path.basename(repoRoot);
const main = files.find(f => path.basename(f, '.csproj') === repoName) || files[0];

console.log(`Bumping version in: ${path.relative(repoRoot, main)}`);

let content = fs.readFileSync(main, 'utf8');

const versionRegex = /<Version>([^<]*)<\/Version>/;
const fileVersionRegex = /<FileVersion>([^<]*)<\/FileVersion>/;

if (versionRegex.test(content)) {
  content = content.replace(versionRegex, `<Version>${newVersion}</Version>`);
  console.log(`<Version> -> ${newVersion}`);
} else if (fileVersionRegex.test(content)) {
  content = content.replace(fileVersionRegex, `<FileVersion>${newVersion}</FileVersion>`);
  console.log(`<FileVersion> -> ${newVersion}`);
} else {
  // Insert <Version> after <AssemblyVersion> or as first property
  const insertAfter = /<AssemblyVersion>[^<]*<\/AssemblyVersion>/;
  if (insertAfter.test(content)) {
    content = content.replace(insertAfter, `$&\n\t\t<Version>${newVersion}</Version>`);
  } else {
    content = content.replace(/<PropertyGroup>/, `<PropertyGroup>\n\t\t<Version>${newVersion}</Version>`);
  }
  console.log(`<Version> inserted: ${newVersion}`);
}

fs.writeFileSync(main, content, 'utf8');
console.log('Done.');
