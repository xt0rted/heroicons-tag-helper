const { readdir, readFile, writeFile } = require('fs/promises');
const { basename, join } = require('path');

const ICON_DIR = './node_modules/heroicons';

const ICON_VARIANTS = [
  { name: 'outline', height: 24, width: 24 },
  { name: 'solid', height: 20, width: 20 },
];

function pascalCase(val) {
  return val
    .split('-')
    .map(v => v.charAt(0).toUpperCase() + v.slice(1))
    .join('');
}

async function processIcons() {
  const data = {};

  const files = await readdir(join(ICON_DIR, ICON_VARIANTS[0].name));

  for (let index = 0; index < files.length; index++) {
    const {
      id,
      details,
    } = await processVariants(files[index]);

    data[id] = details;
  }

  return data;
}

async function processVariants(file) {
  const id = basename(file, '.svg');

  const icon = {
    id,
    details: {
      id,
      name: pascalCase(id),
    },
  };

  for (const variation of ICON_VARIANTS) {
    const details = await fileDetails(variation.name, variation.height, variation.width, file);

    icon.details[variation.name] = details;
  }

  return icon;
}

async function fileDetails(variant, height, width, file) {
  const content = await readFile(join(ICON_DIR, variant, file), { encoding: 'utf8' });

  const paths = content
    .trim()
    .split('\n')
    .slice(1, -1)
    .map(path => path.trim());

  return {
    path: paths.join(''),
    height,
    width,
  };
}

async function run() {
  const data = await processIcons();

  await writeFile(
    './data.json',
    JSON.stringify(data, null, 2), { encoding: 'utf8' });
}

run();
