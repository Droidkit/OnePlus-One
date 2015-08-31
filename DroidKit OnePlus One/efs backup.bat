@title = EFS Backup"
adb shell "mkdir /sdcard/efs"
adb shell su -c "dd if=/dev/block/mmcblk0p10 of=/sdcard/efs/modemst1.bin bs=512"
adb shell su -c "dd if=/dev/block/mmcblk0p11 of=/sdcard/efs/modemst2.bin bs=512"
