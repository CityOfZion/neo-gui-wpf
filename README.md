<p align="center">
  <img 
    src="http://res.cloudinary.com/vidsy/image/upload/v1503160820/CoZ_Icon_DARKBLUE_200x178px_oq0gxm.png" 
    width="125px"
  >
</p>

<h1 align="center">neo-gui</h1>

<p align="center">
  Full wallet for the <b>NEO</b> blockchain.
</p>

<p align="center">
  <a href="https://travis-ci.org/CityOfZion/neo-gui-wpf">
    <img src="https://travis-ci.org/CityOfZion/neo-gui-wpf.svg?branch=master">
  </a>
</p>

## What?

- Full node wallet for interacting with the [NEO](http://neo.org/) blockchain.
- Port of official [NEO GUI](https://github.com/neo-project/neo-gui) to WPF (Windows Presentation Foundation) using MVVM pattern (Model-View-ViewModel).
- **Note** - Application is still being tested, please only use on testnet or private chains

## Project Setup
### On Linux:

```
yum install leveldb-devel
```

### On Windows:
To build and run locally, you need to clone and build https://github.com/neo-project/leveldb first, 
then copy `libleveldb.dll` to the working directory (i.e. /bin/Debug, /bin/Release)

**Note** - When building, the project file settings must be changed from static library (lib) to dynamic linked library (dll).


## Help

- Open a new [issue](https://github.com/CityOfZion/neo-gui-wpf/issues/new) if you encountered a problem.
- Or ping **@lostfella** or **@AboimPinto** in the [NEO Discord Channel](https://discord.gg/R8v48YA).
- Submitting PRs to the project is always welcome!

## License

- Open-source [MIT](https://github.com/CityOfZion/neo-gui-wpf/blob/master/LICENSE).

